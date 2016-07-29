using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.ActionGroups
{
    public class Bookmarks
    {
        public SubsonicClient Client { get; set; }

        public Bookmarks(SubsonicClient client)
        {
            Client = client;
        }
    }
}
