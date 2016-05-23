using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class IndexesCollection
    {

        public string IgnoredArticles { get; set; }
        public long LastModified { get; set; }
        public IEnumerable<BasicItem> Shortcuts { get; private set; }
        public IEnumerable<string> IndexNames { get; private set; }
        public Dictionary<string, IEnumerable<Artist>> Indexes { get; private set; }
        public IEnumerable<Child> Children { get; private set; }

        public static IndexesCollection Create(XDocument xml)
        {
            XElement root = GetIndexesElement(xml);
            string ignoredArticles = root.Attribute("ignoredArticles").Value;
            long lastModified = Convert.ToInt64(root.Attribute("lastModified").Value);
            IEnumerable<BasicItem> shortcuts = ReadShortcuts(root);
            Dictionary<string, IEnumerable<Artist>> indexes = ReadIndexes(root);
            IEnumerable<string> indexNames = GetIndexNames(indexes);
            IEnumerable<Child> children = ReadChildren(root);
            return new IndexesCollection
            {
                IgnoredArticles = ignoredArticles,
                LastModified = lastModified,
                Children = children,
                IndexNames = indexNames,
                Indexes = indexes,
                Shortcuts = shortcuts
            };
        }

        private static XElement GetIndexesElement(XDocument xml) => xml.Root?.Elements().First();

        private static IEnumerable<BasicItem> ReadShortcuts(XElement xml)
        {
            foreach (XElement shortcut in xml.Elements().Where(x => x.Name.LocalName == "shortcut"))
            {

                    yield return new BasicItem
                    {
                        Id = Convert.ToInt32(shortcut.Attribute("id").Value),
                        Name = shortcut.Attribute("name").Value,
                        Kind = ItemType.Shortcut
                    };
            }
        }
        

        private static IEnumerable<Child> ReadChildren(XElement xml)
        {
            return xml.Elements().Where(x => x.Name.LocalName == "child").Select(Child.Create);
        }

        private static Dictionary<string, IEnumerable<Artist>> ReadIndexes(XElement xml)
        {
            Dictionary<string, IEnumerable<Artist>> dict = new Dictionary<string, IEnumerable<Artist>>();
            foreach (XElement element in xml.Elements().Where(x => x.Name.LocalName == "index"))
            {
                string key = element.Attribute("name").Value;
                IEnumerable<Artist> value = EnumerateIndexChildren(element);
                dict.Add(key, value);
            }
            return dict;
        }

        private static IEnumerable<Artist> EnumerateIndexChildren(XElement xml)
        {
            foreach (XElement element in xml.Elements().Where(x => x.Name.LocalName == "artist"))
            {
                Artist artist = new Artist();
                foreach (XAttribute attribute in element.Attributes())
                {
                    string name = attribute.Name.LocalName;
                    switch (name.ToLower())
                    {
                        case "id":
                            artist.Id = Convert.ToInt32(attribute.Value);
                            break;
                        case "name":
                            artist.Name = attribute.Value;
                            break;
                        case "starred":
                            artist.Starred = DateTime.Parse(attribute.Value);
                            break;
                        case "userrating":
                            artist.UserRating = Convert.ToInt32(attribute.Value);
                            break;
                        case "averagerating":
                            artist.AverageRating = Convert.ToInt32(attribute.Value);
                            break;
                    }
                }
                yield return artist;
            }
        }

        private static IEnumerable<string> GetIndexNames(Dictionary<string, IEnumerable<Artist>> dict)
        {
            foreach (string key in dict.Keys)
            {
                yield return key;
            }
        }
    }
}