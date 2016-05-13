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
                return Kind == comp.Kind 
                    && Id == comp.Id 
                    && Name.Equals(comp.Name);
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
        MusicFolder,
        Shortcut,
        Podcast,
        Audiobook,
        Video
    }
}
