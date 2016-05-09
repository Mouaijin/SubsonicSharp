using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    class Child
    {
        public int Id { get; set; }
        public int Parent { get; set; }
        public bool IsDirectory { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public int Track { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string CoverArt { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
        public string Suffix { get; set; }
        public string TranscodedContentType { get; set; }
        public string TranscodedSuffix { get; set; }
        public int Duration { get; set; }
        public int BitRate { get; set; }
        public string Path { get; set; }
        public bool IsVideo { get; set; }
        public int UserRating { get; set; }
        public int AverageRating { get; set; }
        public ItemType MediaType { get; set; }
        public int DiscNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime Starred { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public long BookmarkPosition { get; set; }
        public int OriginalHeight { get; set; }
        public int OriginalWidth { get; set; }
    }
}
