using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class JukeboxStatus
    {
        public int CurrentIndex { get; set; }
        public bool Playing { get; set; }
        public float Gain { get; set; }
        public long Position { get; set; }

        public static JukeboxStatus Create(XElement xml)
        {
            JukeboxStatus status = new JukeboxStatus();
            if (xml.HasAttribute("currentIndex")) status.CurrentIndex = Convert.ToInt32(xml.Attribute("currentIndex").Value);
            if (xml.HasAttribute("playing")) status.Playing = bool.Parse(xml.Attribute("playing").Value);
            if (xml.HasAttribute("gain")) status.Gain = float.Parse(xml.Attribute("gain").Value);
            if (xml.HasAttribute("position")) status.Position = Convert.ToInt64(xml.Attribute("position").Value);
            return status;
        }
    }
}
