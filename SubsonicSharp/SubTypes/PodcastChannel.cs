using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class PodcastChannel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PodcastStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<PodcastEpisode> Episodes { get; set; }

        public static PodcastChannel Create(XElement xml)
        {
            PodcastChannel channel = new PodcastChannel();
            if (xml.HasAttribute("id")) channel.Id = Convert.ToInt32(xml.Attribute("id").Value);
            channel.Url = xml.AttributeValueOrNull("url");
            channel.Title = xml.AttributeValueOrNull("title");
            channel.Description = xml.AttributeValueOrNull("description");
            channel.ErrorMessage = xml.AttributeValueOrNull("errorMessage");
            if (xml.HasAttribute("status"))
                channel.Status = PodcastEpisode.StringToStatus(xml.Attribute("status").Value);
            channel.Episodes = xml.Elements().Where(x => x.Name.LocalName == "episode").Select(PodcastEpisode.Create);
            return channel;
        }
    }
}
