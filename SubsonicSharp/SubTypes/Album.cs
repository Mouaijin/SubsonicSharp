using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Album : BasicItem
    {
        public string CoverArt { get; set; }
        public int SongCount { get; set; }
        public DateTime Created { get; set; }
        public long Duration { get; set; }
        public string Artist { get; set; } = null;
        public int? ArtistId { get; set; } = null;
        public DateTime? Starred { get; set; } = null;
        public int? Year { get; set; } = null;
        public IEnumerable<Child> Songs = null;

        public static Album Create(XElement xml)
        {
            Album album = new Album();
            album.Id = Convert.ToInt32(xml.Attribute("id").Value);
            album.Name = xml.Attribute("name").Value;
            album.SongCount = Convert.ToInt32(xml.Attribute("songCount").Value);
            album.Duration = Convert.ToInt64(xml.Attribute("duration").Value);
            album.Created = DateTime.Parse(xml.Attribute("created").Value);
            album.Artist = xml.AttributeValueOrNull("artist");
            if (xml.HasAttribute("artistId"))
                album.ArtistId = Convert.ToInt32(xml.Attribute("artistId").Value);
            album.CoverArt = xml.AttributeValueOrNull("coverArt");
            if(xml.HasAttribute("starred"))
                album.Starred = DateTime.Parse(xml.Attribute("starred").Value);
            if (xml.HasAttribute("year"))
                album.Year = Convert.ToInt32(xml.Attribute("year"));
            album.Songs = xml.EnumaerateSongs();
            return album;
        }
    }
}
