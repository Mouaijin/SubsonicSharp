using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class NowPlaying : Child
    {
        public string UserName { get; set; }
        public int MinutesAgo { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}
