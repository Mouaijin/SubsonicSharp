using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.ActionGroups
{
    public class MediaAnnotation
    {
        public SubsonicClient Client { get; set; }

        public MediaAnnotation(SubsonicClient client)
        {
            Client = client;
        }
    }
}
