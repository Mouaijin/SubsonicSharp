using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class VideoInfo
    {
        public int Id { get; set; }
        public IEnumerable<AudioTrack> AudioTracks { get; set; }
        public IEnumerable<Captions> Captions { get; set; }
        public IEnumerable<Conversion> Conversions { get; set; }

        public static VideoInfo Create(XElement xml)
        {
            VideoInfo info = new VideoInfo();
            if (xml.HasAttribute("id")) info.Id = Convert.ToInt32(xml.Attribute("id").Value);
            info.AudioTracks = xml.Elements().Where(x => x.Name.LocalName == "audioTrack").Select(AudioTrack.Create);
            info.Captions = xml.Elements().Where(x => x.Name.LocalName == "captions").Select(SubTypes.Captions.Create);
            info.Conversions = xml.Elements().Where(x => x.Name.LocalName == "conversion").Select(Conversion.Create);
            return info;
        }

    }

    public class AudioTrack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LanguageCode { get; set; }

        public static AudioTrack Create(XElement xml)
        {
            AudioTrack at = new AudioTrack();
            if (xml.HasAttribute("id")) at.Id = Convert.ToInt32(xml.Attribute("id").Value);
            at.Name = xml.AttributeValueOrNull("name");
            at.LanguageCode = xml.AttributeValueOrNull("languageCode");
            return at;
        }
    }

    public class Conversion
    {
        public int Id { get; set; }
        public int BitRate { get; set; }

        public static Conversion Create(XElement xml)
        {
            Conversion c = new Conversion();
            if (xml.HasAttribute("id")) c.Id = Convert.ToInt32(xml.Attribute("id").Value);
            if (xml.HasAttribute("bitRate")) c.BitRate = Convert.ToInt32(xml.Attribute("bitRate").Value);
            return c;
        }
    }

    public class Captions
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Captions Create(XElement xml)
        {
            Captions c = new Captions();
            if (xml.HasAttribute("id")) c.Id = Convert.ToInt32(xml.Attribute("id").Value);
            c.Name = xml.AttributeValueOrNull("name");
            return c;
        }
    }
}
