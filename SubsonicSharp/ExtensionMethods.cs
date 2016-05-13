using System;
using System.Linq;
using System.Xml.Linq;

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
    }
}