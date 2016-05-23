using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Genre
    {
        public string Name { get; set; }
        public int SongCount { get; set; }
        public int AlbumCount { get; set; }

        public static Genre Create(XElement xml)
        {
            return new Genre
            {
                Name = xml.Value,
                AlbumCount = Convert.ToInt32(xml.Attribute("albumCount").Value),
                SongCount = Convert.ToInt32(xml.Attribute("songCount").Value)
            };
        }
    }
}
