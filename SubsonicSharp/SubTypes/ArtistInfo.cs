using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class ArtistInfo
    {
        public string Biography { get; set; } = null;
        public string MusicBrainzId { get; set; } = null;
        public string LastFmUrl { get; set; } = null;
        public string SmallImageUrl { get; set; } = null;
        public string MediumImageUrl { get; set; } = null;
        public string LargeImageUrl { get; set; } = null;
        public IEnumerable<Artist> SimilarArtists { get; set; } = null;

        public static ArtistInfo Create(XElement xml)
        {
            ArtistInfo info = new ArtistInfo
            {
                Biography = xml.DescendantValueOrNull("biography"),
                MusicBrainzId = xml.DescendantValueOrNull("musicBrainzId"),
                LastFmUrl = xml.DescendantValueOrNull("lastFmUrl"),
                SmallImageUrl = xml.DescendantValueOrNull("smallImageUrl"),
                MediumImageUrl = xml.DescendantValueOrNull("mediumImageUrl"),
                LargeImageUrl = xml.DescendantValueOrNull("largeImageUrl"),
                SimilarArtists = xml.EnumerateArtists()
            };
            return info;
        }
    }
}
