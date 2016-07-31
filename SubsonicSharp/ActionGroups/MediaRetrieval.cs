using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class MediaRetrieval
    {
        public SubsonicClient Client { get; private set; }

        public MediaRetrieval(SubsonicClient client)
        {
            Client = client;
        }

        #region Public Methods

        /// <summary>
        /// Returns the url to stream the specified media file
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to stream. Obtained by calls to GetMusicDirectory.</param>
        /// <param name="maxBitRate">If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed. </param>
        /// <param name="format">Specifies the preferred target format (e.g., "mp3" or "flv") in case there are multiple applicable transcodings. Starting with 1.9.0 you can use the special value "raw" to disable transcoding. </param>
        /// <param name="estimateContentLength">If set to "true", the Content-Length HTTP header will be set to an estimated value for transcoded or downsampled media. </param>
        /// <returns>HTTP address string used for streaming this file</returns>
        public string GetStreamingAddress(int id, int maxBitRate = 0, string format = null,
            bool estimateContentLength = false, bool converted = false)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "stream",
                Parameters = new List<RestParameter>
                {
                    new RestParameter {Parameter = "id", Value = id.ToString()}
                }
            };
            if (maxBitRate != 0)
                command.Parameters.Add(new RestParameter("maxBitRate", maxBitRate));
            if (format != null)
                command.Parameters.Add(new RestParameter("format", format));
            if (estimateContentLength)
                command.Parameters.Add(new RestParameter("estimateContentLength", bool.TrueString));
            if(converted) command.AddParameter("converted", true);

            return Client.FormatCommand(command);
        }

        /// <summary>
        /// Returns the url to download the specified media file
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to download. Obtained by calls to GetMusicDirectory.</param>
        /// <returns>HTTP address string used for downloading this file</returns>
        public string GetDownloadAddress(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "download",
                Parameters = {new RestParameter("id", id)}
            };
            return Client.FormatCommand(command);
        }

        /// <summary>
        /// Creates an HLS (HTTP Live Streaming) playlist used for streaming video or audio. HLS is a streaming protocol implemented by Apple and works by breaking the overall stream into a sequence of small HTTP-based file downloads. It's supported by iOS and newer versions of Android. This method also supports adaptive bitrate streaming, see the bitRate parameter. 
        /// </summary>
        /// <param name="id">An int which uniquely identifies the media file to stream.</param>
        /// <param name="bitRate">If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. The server will automatically choose video dimensions that are suitable for the given bitrates.</param>
        /// <param name="height">Since 1.9.0 you may explicitly request a certain width (480) and height (360)</param>
        /// <param name="width">Since 1.9.0 you may explicitly request a certain width (480) and height (360)</param>
        /// <param name="audioTrack"> 	The ID of the audio track to use. See getVideoInfo for how to get the list of available audio tracks for a video. </param>
        /// <returns>An HTTP address string used to access the playlist</returns>
        public string Hls(int id, int bitRate = -1, int height = -1, int width = -1, int audioTrack = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "hls";
            command.AddParameter("id", id);
            if (bitRate != -1)
            {
                if (height != -1 && width != -1)
                {
                    command.AddParameter("bitRate", $"{bitRate}@{width}x{height}");
                }
                else
                {
                    command.AddParameter("bitRate", bitRate);
                }
            }
            if(audioTrack != -1)
                command.AddParameter("audioTrack", audioTrack);
            return Client.FormatCommand(command);
        }

        /// <summary>
        /// Returns a cover art image
        /// </summary>
        /// <param name="id">The ID of a song, album or artist.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <returns>A binary array of image data</returns>
        public byte[] GetCovertArt(int id, int size = -1)
        {
            return GetCovertArtAsync(id, size).Result;
        }

        /// <summary>
        /// Returns a cover art image asynchronously
        /// </summary>
        /// <param name="id">The ID of a song, album or artist.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <returns>A binary array of image data</returns>
        public Task<byte[]> GetCovertArtAsync(int id, int size = -1)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getCoverArt",
                Parameters = {new RestParameter("id", id)}
            };
            if (size > 0)
            {
                command.Parameters.Add(new RestParameter("size", size));
            }
            return Task<byte[]>.Factory.StartNew(() =>
            {
                using (HttpClient http = new HttpClient())
                {
                    http.Timeout = TimeSpan.FromDays(1);
                    return http.GetByteArrayAsync(Client.FormatCommand(command)).Result;
                }
            });
        }

        /// <summary>
        /// Searches for and returns lyrics for a given song.
        /// </summary>
        /// <param name="artist">The artist name</param>
        /// <param name="title">The song title</param>
        /// <returns>A Lyrics object containing the returned lyrics text</returns>
        public Lyrics GetLyrics(string artist = null, string title = null)
        {
            if (artist == null && title == null)
                return null;

            RestCommand command = new RestCommand {MethodName = "getLyrics"};
            if (artist != null)
                command.Parameters.Add(new RestParameter("artist", artist));
            if (title != null)
                command.Parameters.Add(new RestParameter("title", title));

            return new Lyrics(Client.GetResponseXDocument(command).RealRoot());
        }

        /// <summary>
        /// Returns captions (subtitles) for a video. Use getVideoInfo to get a list of available captions. 
        /// </summary>
        /// <param name="id"> 	The ID of the video.</param>
        /// <param name="format">Preferred captions format ("srt" or "vtt").</param>
        /// <returns>A Url string used to retrieve requested subtitle data</returns>
        public string GetCaptions(int id, string format = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getCaptions";
            command.AddParameter("id", id);
            if(!string.IsNullOrEmpty(format)) command.AddParameter("format", format);
            return Client.FormatCommand(command);
        }

        /// <summary>
        /// Returns the avatar (personal image) for a user. 
        /// </summary>
        /// <param name="username">The user in question.</param>
        /// <returns>A binary array of image data</returns>
        public byte[] GetAvatar(string username)
        {
            return GetAvatarAsync(username).Result;
        }
        /// <summary>
        /// Returns the avatar (personal image) for a user asynchronously.
        /// </summary>
        /// <param name="username">The user in question.</param>
        /// <returns>A binary array of image data</returns>
        public Task<byte[]> GetAvatarAsync(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getAvatar";
            command.AddParameter("username", username);
            return Task<byte[]>.Factory.StartNew(() =>
            {
                using (HttpClient http = new HttpClient())
                {
                    http.Timeout = TimeSpan.FromDays(1);
                    return http.GetByteArrayAsync(Client.FormatCommand(command)).Result;
                }
            });
        }

        #endregion Public Methods
    }
}