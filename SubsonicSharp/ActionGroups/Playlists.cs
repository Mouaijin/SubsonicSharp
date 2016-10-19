using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Playlists
    {
        public SubsonicClient Client { get; set; }

        public Playlists(SubsonicClient client)
        {
            Client = client;
        }
        /// <summary>
        /// Returns all playlists a user is allowed to play. 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A collection of Playlist objects the user has access to</returns>
        [ApiLevel(0)]
        public IEnumerable<Playlist> GetPlaylists(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPlaylists";
            command.AddParameter("username", username);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Playlist.Create);
        }
        /// <summary>
        /// Returns a listing of files in a saved playlist. 
        /// </summary>
        /// <param name="id">ID of the playlist to return, as obtained by getPlaylists.</param>
        /// <returns>The requested Playlist object</returns>
        [ApiLevel(0)]
        public Playlist GetPlaylist(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPlaylist";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Playlist.Create).First();
        }
        /// <summary>
        /// Creates a playlist. 
        /// </summary>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="ids">ID of a song in the playlist. Use one songId parameter for each song in the playlist.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(2)]
        public bool CreatePlaylist(string name, IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createPlaylist";
            command.AddParameter("name", name);
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Updates a playlist.
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="ids">ID of a song in the playlist. Use one songId parameter for each song in the playlist.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(2)]
        public bool CreateUpdatePlaylist(int playlistId, IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createPlaylist";
            command.AddParameter("playlistId", playlistId);
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Updates a playlist. Only the owner of a playlist is allowed to update it. 
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="comment">The playlist comment.</param>
        /// <param name="isPublic">true if the playlist should be visible to all users, false otherwise.</param>
        /// <param name="songIdsToAdd">Add this song with this ID to the playlist. Multiple parameters allowed.</param>
        /// <param name="songIdsToRemove">Remove the song at this position in the playlist. Multiple parameters allowed.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(8)]
        public bool UpdatePlaylist(int playlistId, string name = null, string comment = null, bool? isPublic = null,
            IEnumerable<int> songIdsToAdd = null, IEnumerable<int> songIdsToRemove = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "updatePlaylist";
            command.AddParameter("playlistId", playlistId);
            if (!string.IsNullOrEmpty(name)) command.AddParameter("name", name);
            if (!string.IsNullOrEmpty(comment)) command.AddParameter("comment", comment);
            if (isPublic.HasValue) command.AddParameter("public", isPublic.Value.ToString().ToLower());
            if (songIdsToAdd != null)
                foreach (int song in songIdsToAdd)
                {
                    command.AddParameter("songIdToAdd", song);
                }
            if (songIdsToRemove != null)
                foreach (int song in songIdsToRemove)
                {
                    command.AddParameter("songIdToRemove", song);
                }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Deletes a saved playlist. 
        /// </summary>
        /// <param name="id"> 	ID of the playlist to delete, as obtained by getPlaylists.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(2)]
        public bool DeletePlaylist(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deletePlaylist";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}