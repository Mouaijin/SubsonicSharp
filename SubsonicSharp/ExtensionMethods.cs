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

        public static string AttributeValueOrNull(this XElement xml, string name)
        {
            return xml.HasAttribute(name) ? xml.Attribute(name).Value : null;
        }

        public static int? IntAttributeOrNull(this XElement xml, string name)
        {
            string val = xml.AttributeValueOrNull(name);
            int? ret = null;
            if (val != null)
                ret = Convert.ToInt32(val);
            return ret;
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
            return xml.Elements().Where(x => x.Name.LocalName == "artist").Select(Artist.Create);
        }

        public static IEnumerable<Album> EnumerateAlbums(this XElement xml)
        {
            return xml.Elements().Where(x => x.Name.LocalName == "album").Select(Album.Create);
        }
    }
}