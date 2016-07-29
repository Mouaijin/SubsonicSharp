using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class PlayQueue
    {
        public int Current { get; set; }
        public long Position { get; set; }
        public string Username { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public IEnumerable<Child> Items { get; set; }

        public static PlayQueue Create(XElement xml)
        {
            PlayQueue q = new PlayQueue();
            if (xml.HasAttribute("current")) q.Current = Convert.ToInt32(xml.Attribute("current").Value);
            if (xml.HasAttribute("position")) q.Position = Convert.ToInt64(xml.Attribute("position").Value);
            q.Username = xml.AttributeValueOrNull("username");
            if (xml.HasAttribute("changed")) q.Changed = DateTime.Parse(xml.Attribute("changed").Value);
            q.ChangedBy = xml.AttributeValueOrNull("changedBy");
            q.Items = xml.Elements().Select(Child.Create);
            return q;
        }
    }
}
