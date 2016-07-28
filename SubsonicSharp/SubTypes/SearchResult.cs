using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class SearchResult
    {
        public int Offset { get; set; }
        public int TotalHits { get; protected set; }
        public IEnumerable<Child> Items { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Artist> Artists { get; set; }

        public static SearchResult Create(XElement xml)
        {
            SearchResult result = new SearchResult();
            result.Artists = xml.Elements().Where(x => x.Name.LocalName=="artist").Select(delegate(XElement element)
            {
                result.TotalHits++;
                return Artist.Create(element);
            });
            result.Albums = xml.Elements().Where(x => x.Name.LocalName == "album").Select(delegate(XElement element)
            {
                result.TotalHits++;
                return Album.Create(element);
            });
            result.Items = xml.Elements().Where(x => x.Name.LocalName=="song" || x.Name.LocalName=="video").Select(delegate(XElement element)
            {
                result.TotalHits++;
                return Child.Create(element);
            });
            return result;
        }


    }
}