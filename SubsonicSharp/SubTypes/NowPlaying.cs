using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class NowPlaying : Child
    {
        public string UserName { get; set; }
        public int MinutesAgo { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }


        public new static NowPlaying Create(XElement xml)
        {
            NowPlaying playing = new NowPlaying();

            #region AttributeSwitch

            foreach (XAttribute attribute in xml.Attributes())
            {
                string name = attribute.Name.LocalName;
                //There is 100% a better way to do this, but at least this is simple and guarantees O(n) at worst
                switch (name.ToLower())
                {
                    case "id":
                        playing.Id = Convert.ToInt32(attribute.Value);
                        break;
                    case "parent":
                        playing.Parent = Convert.ToInt32(attribute.Value);
                        break;
                    case "album":
                        playing.Album = attribute.Value;
                        break;
                    case "artist":
                        playing.Artist = attribute.Value;
                        break;
                    case "track":
                        playing.Track = Convert.ToInt32(attribute.Value);
                        break;
                    case "year":
                        playing.Year = Convert.ToInt32(attribute.Value);
                        break;
                    case "genre":
                        playing.Genre = attribute.Value;
                        break;
                    case "coverart":
                        playing.CoverArt = attribute.Value;
                        break;
                    case "size":
                        playing.Size = Convert.ToInt64(attribute.Value);
                        break;
                    case "contenttype":
                        playing.ContentType = attribute.Value;
                        break;
                    case "suffix":
                        playing.Suffix = attribute.Value;
                        break;
                    case "transcodedcontenttype":
                        playing.TranscodedContentType = attribute.Value;
                        break;
                    case "transcodedsuffix":
                        playing.TranscodedSuffix = attribute.Value;
                        break;
                    case "duration":
                        playing.Duration = Convert.ToInt32(attribute.Value);
                        break;
                    case "bitrate":
                        playing.BitRate = Convert.ToInt32(attribute.Value);
                        break;
                    case "path":
                        playing.Path = attribute.Value;
                        break;
                    case "isvideo":
                        playing.IsVideo = Convert.ToBoolean(attribute.Value);
                        break;
                    case "userrating":
                        playing.UserRating = Convert.ToInt32(attribute.Value);
                        break;
                    case "averagerating":
                        playing.AverageRating = Convert.ToInt32(attribute.Value);
                        break;
                    case "discnumber":
                        playing.DiscNumber = Convert.ToInt32(attribute.Value);
                        break;
                    case "created":
                        playing.Created = Convert.ToDateTime(attribute.Value);
                        break;
                    case "starred":
                        playing.Starred = Convert.ToDateTime(attribute.Value);
                        break;
                    case "albumid":
                        playing.AlbumId = Convert.ToInt32(attribute.Value);
                        break;
                    case "artistid":
                        playing.ArtistId = Convert.ToInt32(attribute.Value);
                        break;
                    case "type":
                        playing.MediaType = attribute.Value.ToMediaType();
                        break;
                    case "bookmarkposition":
                        playing.BookmarkPosition = Convert.ToInt64(attribute.Value);
                        break;
                    case "originalwidth":
                        playing.OriginalWidth = Convert.ToInt32(attribute.Value);
                        break;
                    case "originalheight":
                        playing.OriginalHeight = Convert.ToInt32(attribute.Value);
                        break;
                    case "username":
                        playing.UserName = attribute.Value;
                        break;
                    case "minutesAgo":
                        playing.MinutesAgo = Convert.ToInt32(attribute.Value);
                        break;
                    case "playerId":
                        playing.PlayerId = Convert.ToInt32(attribute.Value);
                        break;
                    case "playerName":
                        playing.PlayerName = attribute.Value;
                        break;
                }
            }

            #endregion AttributeSwitch

            return playing;
        }
    }
}
