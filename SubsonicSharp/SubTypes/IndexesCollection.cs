using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    class IndexesCollection
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

        private static XElement GetIndexesElement(XDocument xml)
        {
            return xml.Root?.Descendants("indexes").Single();
        }

        private static IEnumerable<BasicItem> ReadShortcuts(XElement xml)
        {
            foreach (XElement shortcut in xml.Descendants("shortcut"))
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
            foreach (XElement child in xml.Descendants("child"))
            {
                yield return Child.Create(child);
            }
        }

        private static Dictionary<string, IEnumerable<Artist>> ReadIndexes(XElement xml)
        {
            throw new NotImplementedException();
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