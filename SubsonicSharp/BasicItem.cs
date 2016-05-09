using System;

namespace SubsonicSharp
{
    public class BasicItem
    {
        public ItemType Kind { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        private string KindToString()
        {
            switch (Kind)
            {
                case ItemType.Artist:
                    return "Artist";
                case ItemType.Album:
                    return "Album";
                case ItemType.Directory:
                    return "Directory";
                case ItemType.MusicFolder:
                    return "MusicFolder";
                case ItemType.Playlist:
                    return "Playlist";
                case ItemType.Song:
                    return "Song";
                default:
                    return "Item";
            }
        }

        public override string ToString()
        {
            return $"{KindToString()}: {Name}, {Id}";
        }

        public override bool Equals(object obj)
        {
            try
            {
                BasicItem comp = (BasicItem) obj;
                if (Kind != comp.Kind)
                    return false;
                if (Id != comp.Id)
                    return false;
                return Name.Equals(comp.Name);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public enum ItemType
    {
        Artist,
        Song,
        Album,
        Directory,
        Playlist,
        MusicFolder
    }
}
