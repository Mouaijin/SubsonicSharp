using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Artist : BasicItem
    {
        public DateTime? Starred { get; set; } = null;
        public int? UserRating { get; set; } = null;
        public int? AverageRating { get; set; } = null;
        public int? AlbumCount { get; set; } = null;
        public IEnumerable<Album> Albums { get; set; } = null;

        public static Artist Create(XElement xml)
        {

            Artist artist = new Artist
            {
                Id = Convert.ToInt32(xml.Attribute("id").Value),
                Name = xml.Attribute("name").Value,
                Kind = ItemType.Artist,
                UserRating = xml.IntAttributeOrNull("userRating"),
                AverageRating = xml.IntAttributeOrNull("averageRating"),
                AlbumCount = xml.IntAttributeOrNull("albumCount"),
                Starred = xml.DateTimeAttributeOrNull("starred"),
                Albums = xml.Elements().Where(x => x.Name.LocalName == "album").Select(Album.Create)
            };
            return artist;
        }
    }
}
