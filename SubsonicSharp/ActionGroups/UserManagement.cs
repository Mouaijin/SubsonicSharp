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

        public User GetUser(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getUser";
            command.AddParameter("username", username);
            return User.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        public IEnumerable<User> GetUsers()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getUsers";
            foreach (XElement element in Client.GetResponseXDocument(command).RealRoot().Elements())
            {
                yield return User.Create(element);
            }
        }

        public bool CreateUser(string username, string password, string email, bool ldapAuthenticated = false,
            bool adminRole = false, bool settingsRole = false, bool streamRole = false, bool jukeboxRole = false,
            bool downloadRole = false, bool uploadRole = false, bool playlistRole = false, bool coverArtRole = false,
            bool commentRole = false, bool podcastRole = false, bool shareRole = false, bool videoConversionRole = false,
            int[] musicFolderIds = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createUser";
            command.AddParameter("username", username);
            command.AddParameter("password",password);
            command.AddParameter("email",email);
            command.AddParameter("ldapAuthenticated", ldapAuthenticated);
            command.AddParameter("adminRole", adminRole);
            command.AddParameter("settingsRole",settingsRole);
            command.AddParameter("streamRole",streamRole);
            command.AddParameter("jukeboxRole", jukeboxRole);
            command.AddParameter("downloadRole", downloadRole);
            command.AddParameter("uploadRole", uploadRole);
            command.AddParameter("playlistRole", playlistRole);
            command.AddParameter("coverArtRole",coverArtRole);
            command.AddParameter("commentRole", commentRole);
            command.AddParameter("podcastRole", podcastRole);
            command.AddParameter("shareRole",shareRole);
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
        public bool UpdateUser(string username, string password = null, string email = null, bool ldapAuthenticated = false,
        bool adminRole = false, bool settingsRole = false, bool streamRole = false, bool jukeboxRole = false,
        bool downloadRole = false, bool uploadRole = false, bool playlistRole = false, bool coverArtRole = false,
        bool commentRole = false, bool podcastRole = false, bool shareRole = false, bool videoConversionRole = false,
        int[] musicFolderIds = null, int maxBitRate = 0)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "updateUser";
            command.AddParameter("username", username);
            if(!string.IsNullOrEmpty(password))command.AddParameter("password", password);
            if(!string.IsNullOrEmpty(email))command.AddParameter("email", email);
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
            if(maxBitRate != 0)command.AddParameter("maxBitRate", maxBitRate);
            if (musicFolderIds != null)
            {
                foreach (int folderId in musicFolderIds)
                {
                    command.AddParameter("musicFolderId", folderId);
                }
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool DeleteUser(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deleteUser";
            command.AddParameter("username", username);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool ChangePassword(string username, string newPassword)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "changePassword";
            command.AddParameter("username", username);
            command.AddParameter("password",newPassword);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }

}
