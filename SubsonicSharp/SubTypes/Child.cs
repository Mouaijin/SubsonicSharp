using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Child
    {
        public int Id { get; set; } //Required
        public bool IsDirectory { get; set; } //Required
        public string Title { get; set; } //Required
        //Optional:
        public int? Parent { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public int? Track { get; set; }
        public int? Year { get; set; }
        public string Genre { get; set; }
        public string CoverArt { get; set; }
        public long? Size { get; set; }
        public string ContentType { get; set; }
        public string Suffix { get; set; }
        public string TranscodedContentType { get; set; }
        public string TranscodedSuffix { get; set; }
        public int? Duration { get; set; }
        public int? BitRate { get; set; }
        public string Path { get; set; }
        public bool? IsVideo { get; set; }
        public int? UserRating { get; set; }
        public int? AverageRating { get; set; }
        public ItemType? MediaType { get; set; }
        public int? DiscNumber { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Starred { get; set; }
        public int? AlbumId { get; set; }
        public int? ArtistId { get; set; }
        public long? BookmarkPosition { get; set; }
        public int? OriginalHeight { get; set; }
        public int? OriginalWidth { get; set; }

        public static Child Create(XElement xml)
        {
            Child child = new Child();

            #region AttributeSwitch

            foreach (XAttribute attribute in xml.Attributes())
            {
                string name = attribute.Name.LocalName;
                //There is 100% a better way to do this, but at least this is simple and guarantees O(n) at worst
                switch (name.ToLower())
                {
                    case "id":
                        child.Id = Convert.ToInt32(attribute.Value);
                        break;
                    case "parent":
                        child.Parent = Convert.ToInt32(attribute.Value);
                        break;
                    case "album":
                        child.Album = attribute.Value;
                        break;
                    case "artist":
                        child.Artist = attribute.Value;
                        break;
                    case "track":
                        child.Track = Convert.ToInt32(attribute.Value);
                        break;
                    case "year":
                        child.Year = Convert.ToInt32(attribute.Value);
                        break;
                    case "genre":
                        child.Genre = attribute.Value;
                        break;
                    case "coverart":
                        child.CoverArt = attribute.Value;
                        break;
                    case "size":
                        child.Size = Convert.ToInt64(attribute.Value);
                        break;
                    case "contenttype":
                        child.ContentType = attribute.Value;
                        break;
                    case "suffix":
                        child.Suffix = attribute.Value;
                        break;
                    case "transcodedcontenttype":
                        child.TranscodedContentType = attribute.Value;
                        break;
                    case "transcodedsuffix":
                        child.TranscodedSuffix = attribute.Value;
                        break;
                    case "duration":
                        child.Duration = Convert.ToInt32(attribute.Value);
                        break;
                    case "bitrate":
                        child.BitRate = Convert.ToInt32(attribute.Value);
                        break;
                    case "path":
                        child.Path = attribute.Value;
                        break;
                    case "isvideo":
                        child.IsVideo = Convert.ToBoolean(attribute.Value);
                        break;
                    case "userrating":
                        child.UserRating = Convert.ToInt32(attribute.Value);
                        break;
                    case "averagerating":
                        child.AverageRating = Convert.ToInt32(attribute.Value);
                        break;
                    case "discnumber":
                        child.DiscNumber = Convert.ToInt32(attribute.Value);
                        break;
                    case "created":
                        child.Created = Convert.ToDateTime(attribute.Value);
                        break;
                    case "starred":
                        child.Starred = Convert.ToDateTime(attribute.Value);
                        break;
                    case "albumid":
                        child.AlbumId = Convert.ToInt32(attribute.Value);
                        break;
                    case "artistid":
                        child.ArtistId = Convert.ToInt32(attribute.Value);
                        break;
                    case "type":
                        child.MediaType = attribute.Value.ToMediaType();
                        break;
                    case "bookmarkposition":
                        child.BookmarkPosition = Convert.ToInt64(attribute.Value);
                        break;
                    case "originalwidth":
                        child.OriginalWidth = Convert.ToInt32(attribute.Value);
                        break;
                    case "originalheight":
                        child.OriginalHeight = Convert.ToInt32(attribute.Value);
                        break;
                }
            }

            #endregion AttributeSwitch

            return child;
        }
    }
}