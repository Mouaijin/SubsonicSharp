using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.ActionGroups
{
    public class Sharing
    {
        public SubsonicClient Client { get; set; }

        public Sharing(SubsonicClient client)
        {
            Client = client;
        }
    }
}
