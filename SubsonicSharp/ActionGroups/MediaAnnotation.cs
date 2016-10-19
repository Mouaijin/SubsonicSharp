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

        /// <summary>
        /// Attaches a star to a song.
        /// </summary>
        /// <param name="ids">The ID of the file (song) or folder (album/artist) to star. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool Star(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "star";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Attaches a star to an album.
        /// </summary>
        /// <param name="ids">The ID of the file (song) or folder (album/artist) to star. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool StarAlbums(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "star";
            foreach (int id in ids)
            {
                command.AddParameter("albumId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Attaches a star to an artist. 
        /// </summary>
        /// <param name="ids">The ID of the file (song) or folder (album/artist) to star. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool StarArtists(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "star";
            foreach (int id in ids)
            {
                command.AddParameter("artistId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Removes the star from a song.
        /// </summary>
        /// <param name="ids">The ID of the file (song) or folder (album/artist) to unstar. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool Unstar(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "unstar";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Removes the star from an album.
        /// </summary>
        /// <param name="ids">The ID of the file (song) or folder (album/artist) to unstar. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool UnstarAlbums(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "unstar";
            foreach (int id in ids)
            {
                command.AddParameter("albumId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Removes the star from an artist. 
        /// </summary>
        /// <param name="ids">The ID of the file (song) or folder (album/artist) to unstar. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool UnstarArtists(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "unstar";
            foreach (int id in ids)
            {
                command.AddParameter("artistId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Sets the rating for a music file.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file (song) or folder (album/artist) to rate.</param>
        /// <param name="rating">The rating between 1 and 5 (inclusive), or 0 to remove the rating.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(6)]
        public bool SetRating(int id, int rating)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "setRating";
            command.AddParameter("id", id);
            command.AddParameter("rating", rating);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// "Scrobbles" a given music file on last.fm. Requires that the user has configured his/her last.fm credentials on the Subsonic server (Settings &gt; Personal).
        /// Since 1.11.0 this method will also update the play count and last played timestamp for the song and album. It will also make the song appear in the "Now playing" page in the web app, and appear in the list of songs returned by getNowPlaying. 
        /// </summary>
        /// <param name="id">An int which uniquely identifies the file to scrobble.</param>
        /// <param name="time">The time at which the song was listened to</param>
        /// <param name="submission">Whether this is a "submission" or a "now playing" notification.</param>
        /// <returns></returns>
        [ApiLevel(5)]
        public bool Scrobble(int id, DateTime? time = null, bool submission = true)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "scrobble";
            command.AddParameter("id", id);
            if (time.HasValue) command.AddParameter("time", time.Value.ToSecondsFrom1970().ToString());
            if (submission == false) command.AddParameter("submission", false);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}