using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    public class Artist : BasicItem
    {
        public DateTime? Starred { get; set; } = null;
        public int? UserRating { get; set; } = null;
        public int? AverageRating { get; set; } = null;
    }
}
