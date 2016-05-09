using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class PlayQueue
    {
        public int Current { get; set; }
        public long Position { get; set; }
        public string Username { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public IEnumerable<Child> Items { get; set; }
    }
}
