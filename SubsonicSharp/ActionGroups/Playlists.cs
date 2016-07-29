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

        public IEnumerable<Playlist> GetPlaylists(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPlaylists";
            command.AddParameter("username", username);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Playlist.Create);
        }

        public Playlist GetPlaylist(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPlaylist";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Playlist.Create).First();
        }

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

        public bool DeletePlaylist(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deletePlaylist";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}