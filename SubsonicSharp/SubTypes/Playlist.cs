using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class Playlist
    {
        public string Comment { get; set; }
        public string Owner { get; set; }
        public bool Public { get; set; }
        public int SongCount { get; set; }
        public int Duration { get; set; }
        public DateTime Created { get; set; }
        public string CoverArt { get; set; }
        public IEnumerable<string> AllowedUsers { get; set; }
        public IEnumerable<Child> Items { get; set; }
    }
}
