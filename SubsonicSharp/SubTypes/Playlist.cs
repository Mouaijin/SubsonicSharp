using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Owner { get; set; }
        public bool Public { get; set; }
        public int SongCount { get; set; }
        public int Duration { get; set; }
        public DateTime Created { get; set; }
        public string CoverArt { get; set; }
        public IEnumerable<string> AllowedUsers { get; set; }
        public IEnumerable<Child> Items { get; set; }

        public static Playlist Create(XElement xml)
        {
            Playlist p = new Playlist();
            if (xml.HasAttribute("id")) p.Id = Convert.ToInt32(xml.Attribute("id").Value);
            p.Comment = xml.AttributeValueOrNull("comment");
            p.Owner = xml.AttributeValueOrNull("owner");
            p.CoverArt = xml.AttributeValueOrNull("coverArt");
            if (xml.HasAttribute("public")) p.Public = bool.Parse(xml.Attribute("public").Value);
            if (xml.HasAttribute("songCount")) p.SongCount = Convert.ToInt32(xml.Attribute("songCount").Value);
            if (xml.HasAttribute("duration")) p.Duration = Convert.ToInt32(xml.Attribute("duration").Value);
            if (xml.HasAttribute("created")) p.Created = DateTime.Parse(xml.Attribute("created").Value);
            p.AllowedUsers = xml.Elements().Where(e => e.Name.LocalName == "allowedUser").Select(x => x.Value);
            p.Items = xml.Elements().Where(e => e.Name.LocalName == "entry").Select(Child.Create);
            return p;
        }
    }
}
