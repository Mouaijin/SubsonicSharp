using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp
{
    internal static class ExtensionMethods
    {
        public static bool HasAttribute(this XElement xml, string name)
        {
            string lower = name.ToLower();
            return xml.Attributes().Any(attribute => attribute.Name.ToString().ToLower().Equals(lower));
        }

        public static ItemType ToMediaType(this string str)
        {
            switch (str.ToLower())
            {
                case "podcast":
                    return ItemType.Podcast;
                case "audiobook":
                    return ItemType.Audiobook;
                case "video":
                    return ItemType.Video;
                default:
                    return ItemType.Song;
            }
        }

        public static IEnumerable<Artist> EnumerateArtists(this XElement xml)
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
    }
}