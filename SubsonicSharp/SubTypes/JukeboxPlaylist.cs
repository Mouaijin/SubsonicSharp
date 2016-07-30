using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class JukeboxPlaylist : JukeboxStatus
    {
        public IEnumerable<Child> Items { get; set; }

        public static JukeboxPlaylist Create(XElement xml)
        {
            JukeboxStatus js = JukeboxStatus.Create(xml);
            JukeboxPlaylist jp = new JukeboxPlaylist
            {
                Position = js.Position,
                CurrentIndex = js.CurrentIndex,
                Gain = js.Gain,
                Playing = js.Playing,
                Items = xml.Elements().Select(Child.Create)
            };
            return jp;
        }
    }
}
