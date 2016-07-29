using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.ActionGroups
{
    public class Playlists
    {
        public SubsonicClient Client { get; set; }

        public Playlists(SubsonicClient client)
        {
            Client = client;
        }
    }
}
