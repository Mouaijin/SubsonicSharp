using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class AlbumInfo
    {
        public string Notes { get; set; }
        public string MusicBrainzId { get; set; }
        public string LastFmUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string MediumImageUrl { get; set; }
        public string LargeImageUrl { get; set; }

        public static AlbumInfo Create(XElement xml)
        {
            AlbumInfo info = new AlbumInfo
            {
                Notes = xml.AttributeValueOrNull("notes"),
                MusicBrainzId = xml.AttributeValueOrNull("musicBrainzId"),
                LastFmUrl = xml.AttributeValueOrNull("lastFmUrl"),
                SmallImageUrl = xml.AttributeValueOrNull("smallImageUrl"),
                MediumImageUrl = xml.AttributeValueOrNull("mediumImageUrl"),
                LargeImageUrl = xml.AttributeValueOrNull("largeImageUrl")
            };
            return info;
        }
    }
}
