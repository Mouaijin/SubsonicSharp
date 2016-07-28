using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SubsonicSharp.ActionGroups;
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

        public static string AttributeValueOrNull(this XElement xml, string name)
        {
            return xml.HasAttribute(name) ? xml.Attribute(name).Value : null;
        }

        public static int? IntAttributeOrNull(this XElement xml, string name)
        {
            string str = xml.AttributeValueOrNull(name);
            int? ret = null;
            if (str != null)
                ret = Convert.ToInt32(str);
            return ret;
        }

        public static DateTime? DateTimeAttributeOrNull(this XElement xml, string name)
        {
            string str = xml.AttributeValueOrNull(name);
            DateTime? ret = null;
            if (str != null)
                ret = DateTime.Parse(str);
            return ret;
        }

        public static string DescendantValueOrNull(this XElement xml, string elementName)
        {
            return xml.Elements().Any(x => x.Name.LocalName == elementName) 
                ? xml.Elements().First(x => x.Name.LocalName == elementName).Value 
                : null;
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
            return xml.Elements().Where(x => x.Name.LocalName == "artist"
                                             || x.Name.LocalName == "similarArtist")
                                 .Select(Artist.Create);
        }

        public static IEnumerable<Album> EnumerateAlbums(this XElement xml)
        {
            return xml.Elements().Where(x => x.Name.LocalName == "album").Select(Album.Create);
        }

        public static IEnumerable<Child> EnumerateChildren(this XElement xml)
        {
            return
                xml.Elements()
                    .Where(x => x.Name.LocalName == "song"
                                || x.Name.LocalName == "child"
                                || x.Name.LocalName == "video")
                    .Select(Child.Create);
        }

        public static string ConvertToString(this ListOrdering type)
        {
            switch (type)
            {
                case ListOrdering.Random:
                    return "random";
                case ListOrdering.Newest:
                    return "newest";
                case ListOrdering.Highest:
                    return "highest";
                case ListOrdering.Frequent:
                    return "frequent";
                case ListOrdering.Recent:
                    return "recent";
                case ListOrdering.AlphabeticalByName:
                    return "alphabeticalByName";
                case ListOrdering.AlphabeticalByArtist:
                    return "alphabeticalByArtist";
                case ListOrdering.Starred:
                    return "starred";
                case ListOrdering.ByYear:
                    return "byYear";
                case ListOrdering.ByGenre:
                    return "byGenre";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}