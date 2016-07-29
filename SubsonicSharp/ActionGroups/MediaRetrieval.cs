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
            bool estimateContentLength = false)
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

        #endregion Public Methods
    }
}