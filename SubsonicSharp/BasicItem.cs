using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp
{
    class BasicItem
    {
        public ItemType Kind { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    enum ItemType
    {
        Artist,
        Song,
        Album,
        Directory,
        Playlist
    }
}
