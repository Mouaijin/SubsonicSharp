using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class PodcastEpisode : Child
    {
        public int StreamId { get; set; }
        public string Description { get; set; }
        public PodcastStatus Status { get; set; }
        public DateTime PublishDate { get; set; }

        public static PodcastEpisode Create(XElement xml)
        {
            PodcastEpisode episode = new PodcastEpisode();

            #region AttributeSwitch

            foreach (XAttribute attribute in xml.Attributes())
            {
                string name = attribute.Name.LocalName;
                //There is 100% a better way to do this, but at least this is simple
                switch (name.ToLower())
                {
                    case "streamId":
                        episode.StreamId = Convert.ToInt32(attribute.Value);
                        break;
                    case "description":
                        episode.Description = attribute.Value;
                        break;
                    case "status":
                        episode.Status = StringToStatus(attribute.Value);
                        break;
                    case "publishDate":
                        episode.PublishDate = DateTime.Parse(attribute.Value);
                        break;
                    case "id":
                        episode.Id = Convert.ToInt32(attribute.Value);
                        break;
                    case "parent":
                        episode.Parent = Convert.ToInt32(attribute.Value);
                        break;
                    case "album":
                        episode.Album = attribute.Value;
                        break;
                    case "artist":
                        episode.Artist = attribute.Value;
                        break;
                    case "track":
                        episode.Track = Convert.ToInt32(attribute.Value);
                        break;
                    case "year":
                        episode.Year = Convert.ToInt32(attribute.Value);
                        break;
                    case "genre":
                        episode.Genre = attribute.Value;
                        break;
                    case "coverart":
                        episode.CoverArt = attribute.Value;
                        break;
                    case "size":
                        episode.Size = Convert.ToInt64(attribute.Value);
                        break;
                    case "contenttype":
                        episode.ContentType = attribute.Value;
                        break;
                    case "suffix":
                        episode.Suffix = attribute.Value;
                        break;
                    case "transcodedcontenttype":
                        episode.TranscodedContentType = attribute.Value;
                        break;
                    case "transcodedsuffix":
                        episode.TranscodedSuffix = attribute.Value;
                        break;
                    case "duration":
                        episode.Duration = Convert.ToInt32(attribute.Value);
                        break;
                    case "bitrate":
                        episode.BitRate = Convert.ToInt32(attribute.Value);
                        break;
                    case "path":
                        episode.Path = attribute.Value;
                        break;
                    case "isvideo":
                        episode.IsVideo = Convert.ToBoolean(attribute.Value);
                        break;
                    case "userrating":
                        episode.UserRating = Convert.ToInt32(attribute.Value);
                        break;
                    case "averagerating":
                        episode.AverageRating = Convert.ToInt32(attribute.Value);
                        break;
                    case "discnumber":
                        episode.DiscNumber = Convert.ToInt32(attribute.Value);
                        break;
                    case "created":
                        episode.Created = Convert.ToDateTime(attribute.Value);
                        break;
                    case "starred":
                        episode.Starred = Convert.ToDateTime(attribute.Value);
                        break;
                    case "albumid":
                        episode.AlbumId = Convert.ToInt32(attribute.Value);
                        break;
                    case "artistid":
                        episode.ArtistId = Convert.ToInt32(attribute.Value);
                        break;
                    case "type":
                        episode.MediaType = attribute.Value.ToMediaType();
                        break;
                    case "bookmarkposition":
                        episode.BookmarkPosition = Convert.ToInt64(attribute.Value);
                        break;
                    case "originalwidth":
                        episode.OriginalWidth = Convert.ToInt32(attribute.Value);
                        break;
                    case "originalheight":
                        episode.OriginalHeight = Convert.ToInt32(attribute.Value);
                        break;
                }
            }

            #endregion AttributeSwitch

            return episode;
        }

        public static PodcastStatus StringToStatus(string status)
        {
            switch (status)
            {
                case "new":return PodcastStatus.New;
                case "downloading":return PodcastStatus.Downloading;
                case "completed":return PodcastStatus.Completed;
                case "error":return PodcastStatus.Error;
                case "deleted":return PodcastStatus.Deleted;
                case "skipped":return PodcastStatus.Skipped;
                default:
                    throw new Exception("Podcast status could not be enumerated: \""+status+"\"");
            }
        }
    }

    public enum PodcastStatus
    {
        New,
        Downloading,
        Completed,
        Error,
        Deleted,
        Skipped
    }
}