using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Share
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public DateTime LastVisited { get; set; }
        public int VisitCount { get; set; }
        public IEnumerable<Child> Entries { get; set; }

        public static Share Create(XElement xml)
        {
            Share share = new Share();
            if (xml.HasAttribute("id")) share.Id = Convert.ToInt32(xml.Attribute("id").Value);
            share.Url = xml.AttributeValueOrNull("url");
            share.Description = xml.AttributeValueOrNull("description");
            share.Username = xml.AttributeValueOrNull("username");
            if (xml.HasAttribute("created")) share.Created = DateTime.Parse(xml.Attribute("created").Value);
            if (xml.HasAttribute("expires")) share.Expires = DateTime.Parse(xml.Attribute("expires").Value);
            if (xml.HasAttribute("lastVisited")) share.LastVisited = DateTime.Parse(xml.Attribute("lastVisited").Value);
            if (xml.HasAttribute("visitCount")) share.VisitCount = Convert.ToInt32(xml.Attribute("visitCount").Value);
            share.Entries = xml.Elements().Select(Child.Create);
            return share;
        }
    }
}
