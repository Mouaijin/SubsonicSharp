using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class Album : BasicItem
    {
        public string CoverArt { get; set; }
        public int SongCount { get; set; }
        public DateTime Created { get; set; }
        public long Duration { get; set; }
        public string Artist { get; set; }
        public int ArtistId { get; set; }
    }
}
