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
            if (xml.HasAttribute("name")) album.Name = xml.Attribute("name").Value;
            if (xml.HasAttribute("title")) album.Name = xml.Attribute("title").Value;
            if (xml.HasAttribute("songCount")) album.SongCount = Convert.ToInt32(xml.Attribute("songCount").Value);
            if (xml.HasAttribute("duration")) album.Duration = Convert.ToInt64(xml.Attribute("duration").Value);
            if (xml.HasAttribute("created")) album.Created = DateTime.Parse(xml.Attribute("created").Value);
            if (xml.HasAttribute("artist")) album.Artist = xml.AttributeValueOrNull("artist");
            if (xml.HasAttribute("artistId"))
                album.ArtistId = Convert.ToInt32(xml.Attribute("artistId").Value);
            if (xml.HasAttribute("coverArt")) album.CoverArt = xml.AttributeValueOrNull("coverArt");
            if (xml.HasAttribute("starred"))
                album.Starred = DateTime.Parse(xml.Attribute("starred").Value);
            if (xml.HasAttribute("year"))
                album.Year = Convert.ToInt32(xml.Attribute("year").Value);
            album.Songs = xml.EnumerateChildren();
            return album;
        }
    }
}