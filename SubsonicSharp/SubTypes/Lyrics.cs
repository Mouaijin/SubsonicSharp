using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Lyrics
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Lyrics(XElement xml)
        {
            if (xml.HasAttribute("artist"))
                Artist = xml.Attribute("artist").Value;
            if (xml.HasAttribute("title"))
                Title = xml.Attribute("title").Value;
            Text = xml.Value;
        }
    }
}
