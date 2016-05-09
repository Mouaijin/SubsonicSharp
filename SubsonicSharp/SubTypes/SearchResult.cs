using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class SearchResult
    {
        public int Offset { get; set; }
        public int TotalHits { get; set; }
        public IEnumerable<Child> Items{ get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
    }
}
