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
            result.Artists = xml.Elements("artist").Select(delegate(XElement element)
            {
                result.TotalHits++;
                return Artist.Create(element);
            });
            result.Albums = xml.Elements("album").Select(delegate(XElement element)
            {
                result.TotalHits++;
                return Album.Create(element);
            });
            result.Items = xml.Elements("song").Select(delegate(XElement element)
            {
                result.TotalHits++;
                return Child.Create(element);
            });
            return result;
        }
    }
}