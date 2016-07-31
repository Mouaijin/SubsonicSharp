using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Bookmarks
    {
        public SubsonicClient Client { get; set; }

        public Bookmarks(SubsonicClient client)
        {
            Client = client;
        }
        /// <summary>
        /// Returns all bookmarks for this user. A bookmark is a position within a certain media file. 
        /// </summary>
        /// <returns>A collection of Bookmarks objects for all bookmarks in the system</returns>
        public IEnumerable<Bookmark> GetBookmarks()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getBookmarks";
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Bookmark.Create);
        }
        /// <summary>
        /// Creates or updates a bookmark (a position within a media file). Bookmarks are personal and not visible to other users.
        /// </summary>
        /// <param name="id">ID of the media file to bookmark. If a bookmark already exists for this file it will be overwritten.</param>
        /// <param name="position">The position (in milliseconds) within the media file.</param>
        /// <param name="comment">A user-defined comment.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool CreateBookmark(int id, long position, string comment = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createBookmark";
            command.AddParameter("id", id);
            command.AddParameter("position", position.ToString());
            if(!string.IsNullOrEmpty(comment)) command.AddParameter("comment", comment);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Deletes the bookmark for a given file.
        /// </summary>
        /// <param name="id">ID of the media file for which to delete the bookmark. Other users' bookmarks are not affected.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool DeleteBookmark(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deleteBookmark";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";

        }
        /// <summary>
        /// Returns the state of the play queue for this user (as set by savePlayQueue). This includes the tracks in the play queue, the currently playing track, and the position within this track. Typically used to allow a user to move between different clients/apps while retaining the same play queue (for instance when listening to an audio book). 
        /// </summary>
        /// <returns>A PlayQueue object for the current play queue for the current user</returns>
        public PlayQueue GetPlayQueue()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPlayQueue";
            return PlayQueue.Create(Client.GetResponseXDocument(command).RealRoot());
        }
        /// <summary>
        /// Saves the state of the play queue for this user. This includes the tracks in the play queue, the currently playing track, and the position within this track. Typically used to allow a user to move between different clients/apps while retaining the same play queue (for instance when listening to an audio book). 
        /// </summary>
        /// <param name="ids">ID of a song in the play queue. Use one id parameter for each song in the play queue.</param>
        /// <param name="currentId">The ID of the current playing song.</param>
        /// <param name="position">The position in milliseconds within the currently playing song.</param>
        /// <returns></returns>
        public bool SavePlayQueue(IEnumerable<int> ids, int currentId = -1, long position = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "savePlayQueue";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            if(currentId != -1) command.AddParameter("current", currentId);
            if(position != -1) command.AddParameter("position", position.ToString());
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";

        }
    }
}
