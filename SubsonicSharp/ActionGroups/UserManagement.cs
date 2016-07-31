using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class UserManagement
    {
        public SubsonicClient Client { get; set; }

        public UserManagement(SubsonicClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Get details about a given user, including which authorization roles and folder access it has. Can be used to enable/disable certain features in the client, such as jukebox control.
        /// </summary>
        /// <param name="username">The name of the user to retrieve. You can only retrieve your own user unless you have admin privileges. </param>
        /// <returns>A User object with the specified User's information</returns>
        public User GetUser(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getUser";
            command.AddParameter("username", username);
            return User.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        /// <summary>
        /// Get details about all users, including which authorization roles and folder access they have. Only users with admin privileges are allowed to call this method. 
        /// </summary>
        /// <returns>A collection of User objects for all users in the system</returns>
        public IEnumerable<User> GetUsers()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getUsers";
            foreach (XElement element in Client.GetResponseXDocument(command).RealRoot().Elements())
            {
                yield return User.Create(element);
            }
        }

        /// <summary>
        /// Creates a new Subsonic user, using the following parameters:
        /// </summary>
        /// <param name="username">The name of the new user.</param>
        /// <param name="password">The password of the new user.</param>
        /// <param name="email">The email address of the new user.</param>
        /// <param name="ldapAuthenticated">Whether the user is authenicated in LDAP.</param>
        /// <param name="adminRole">Whether the user is administrator.</param>
        /// <param name="settingsRole">Whether the user is allowed to change personal settings and password.</param>
        /// <param name="streamRole">Whether the user is allowed to play files.</param>
        /// <param name="jukeboxRole">Whether the user is allowed to play files in jukebox mode.</param>
        /// <param name="downloadRole">Whether the user is allowed to download files.</param>
        /// <param name="uploadRole">Whether the user is allowed to upload files.</param>
        /// <param name="playlistRole">Whether the user is allowed to create and delete playlists. Since 1.8.0, changing this role has no effect. </param>
        /// <param name="coverArtRole">Whether the user is allowed to change cover art and tags.</param>
        /// <param name="commentRole">Whether the user is allowed to create and edit comments and ratings.</param>
        /// <param name="podcastRole">Whether the user is allowed to administrate Podcasts.</param>
        /// <param name="shareRole">Whether the user is allowed to share files with anyone.</param>
        /// <param name="videoConversionRole">Whether the user is allowed to start video conversions.</param>
        /// <param name="musicFolderIds">IDs of the music folders the user is allowed access to. Include the parameter once for each folder.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool CreateUser(string username, string password, string email, bool ldapAuthenticated = false,
            bool adminRole = false, bool settingsRole = false, bool streamRole = false, bool jukeboxRole = false,
            bool downloadRole = false, bool uploadRole = false, bool playlistRole = false, bool coverArtRole = false,
            bool commentRole = false, bool podcastRole = false, bool shareRole = false, bool videoConversionRole = false,
            int[] musicFolderIds = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createUser";
            command.AddParameter("username", username);
            command.AddParameter("password", password);
            command.AddParameter("email", email);
            command.AddParameter("ldapAuthenticated", ldapAuthenticated);
            command.AddParameter("adminRole", adminRole);
            command.AddParameter("settingsRole", settingsRole);
            command.AddParameter("streamRole", streamRole);
            command.AddParameter("jukeboxRole", jukeboxRole);
            command.AddParameter("downloadRole", downloadRole);
            command.AddParameter("uploadRole", uploadRole);
            command.AddParameter("playlistRole", playlistRole);
            command.AddParameter("coverArtRole", coverArtRole);
            command.AddParameter("commentRole", commentRole);
            command.AddParameter("podcastRole", podcastRole);
            command.AddParameter("shareRole", shareRole);
            command.AddParameter("videoConversionRole", videoConversionRole);
            if (musicFolderIds != null)
            {
                foreach (int folderId in musicFolderIds)
                {
                    command.AddParameter("musicFolderId", folderId);
                }
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Modifies an existing Subsonic user, using the following parameters:
        /// </summary>
        /// <param name="username">The name of the new user.</param>
        /// <param name="password">The password of the new user.</param>
        /// <param name="email">The email address of the new user.</param>
        /// <param name="ldapAuthenticated">Whether the user is authenicated in LDAP.</param>
        /// <param name="adminRole">Whether the user is administrator.</param>
        /// <param name="settingsRole">Whether the user is allowed to change personal settings and password.</param>
        /// <param name="streamRole">Whether the user is allowed to play files.</param>
        /// <param name="jukeboxRole">Whether the user is allowed to play files in jukebox mode.</param>
        /// <param name="downloadRole">Whether the user is allowed to download files.</param>
        /// <param name="uploadRole">Whether the user is allowed to upload files.</param>
        /// <param name="playlistRole">Whether the user is allowed to create and delete playlists. Since 1.8.0, changing this role has no effect. </param>
        /// <param name="coverArtRole">Whether the user is allowed to change cover art and tags.</param>
        /// <param name="commentRole">Whether the user is allowed to create and edit comments and ratings.</param>
        /// <param name="podcastRole">Whether the user is allowed to administrate Podcasts.</param>
        /// <param name="shareRole">Whether the user is allowed to share files with anyone.</param>
        /// <param name="videoConversionRole">Whether the user is allowed to start video conversions.</param>
        /// <param name="musicFolderIds">IDs of the music folders the user is allowed access to. Include the parameter once for each folder.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool UpdateUser(string username, string password = null, string email = null,
            bool ldapAuthenticated = false,
            bool adminRole = false, bool settingsRole = false, bool streamRole = false, bool jukeboxRole = false,
            bool downloadRole = false, bool uploadRole = false, bool playlistRole = false, bool coverArtRole = false,
            bool commentRole = false, bool podcastRole = false, bool shareRole = false, bool videoConversionRole = false,
            int[] musicFolderIds = null, int maxBitRate = 0)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "updateUser";
            command.AddParameter("username", username);
            if (!string.IsNullOrEmpty(password)) command.AddParameter("password", password);
            if (!string.IsNullOrEmpty(email)) command.AddParameter("email", email);
            command.AddParameter("ldapAuthenticated", ldapAuthenticated);
            command.AddParameter("adminRole", adminRole);
            command.AddParameter("settingsRole", settingsRole);
            command.AddParameter("streamRole", streamRole);
            command.AddParameter("jukeboxRole", jukeboxRole);
            command.AddParameter("downloadRole", downloadRole);
            command.AddParameter("uploadRole", uploadRole);
            command.AddParameter("playlistRole", playlistRole);
            command.AddParameter("coverArtRole", coverArtRole);
            command.AddParameter("commentRole", commentRole);
            command.AddParameter("podcastRole", podcastRole);
            command.AddParameter("shareRole", shareRole);
            command.AddParameter("videoConversionRole", videoConversionRole);
            if (maxBitRate != 0) command.AddParameter("maxBitRate", maxBitRate);
            if (musicFolderIds != null)
            {
                foreach (int folderId in musicFolderIds)
                {
                    command.AddParameter("musicFolderId", folderId);
                }
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Deletes an existing Subsonic user, using the following parameters: 
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool DeleteUser(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deleteUser";
            command.AddParameter("username", username);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        /// <summary>
        /// Changes the password of an existing Subsonic user, using the following parameters. You can only change your own password unless you have admin privileges. 
        /// </summary>
        /// <param name="username">The name of the user which should change its password.</param>
        /// <param name="newPassword">The new password of the new user.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool ChangePassword(string username, string newPassword)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "changePassword";
            command.AddParameter("username", username);
            command.AddParameter("password", newPassword);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}