using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class Artist : BasicItem
    {
        public DateTime Starred { get; set; }
        public int UserRating { get; set; }
        public int AverageRating { get; set; }
        public int AlbumCount { get; set; }
        public string CoverArt { get; set; }
    }
}
