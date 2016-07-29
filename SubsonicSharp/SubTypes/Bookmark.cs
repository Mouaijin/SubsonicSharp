using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Bookmark
    {
        public long Position { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public Child MediaFile { get; set; }

        public static Bookmark Create(XElement xml)
        {
            Bookmark book = new Bookmark();
            if (xml.HasAttribute("position")) book.Position = Convert.ToInt64(xml.Attribute("position").Value);
            book.Username = xml.AttributeValueOrNull("username");
            book.Comment = xml.AttributeValueOrNull("comment");
            if (xml.HasAttribute("created")) book.Created = DateTime.Parse(xml.Attribute("created").Value);
            if (xml.HasAttribute("changed")) book.Changed = DateTime.Parse(xml.Attribute("changed").Value);
            book.MediaFile = Child.Create(xml.Elements().First());
            return book;
        }
    }
}
