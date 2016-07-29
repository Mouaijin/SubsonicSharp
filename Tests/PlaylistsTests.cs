using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class PlaylistsTests
    {
        public SubsonicClient Client { get; }
        private int listId;
        public PlaylistsTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void CreatePlaylistTest()
        {
            Assert.IsTrue(Client.Playlists.CreatePlaylist("testpl", new []{2617,999}));
        }

        [TestMethod]
        public void GetPlaylistsTest()
        {
            IEnumerable<Playlist> playlists = Client.Playlists.GetPlaylists("test");
            listId = playlists.First().Id;
            Assert.IsTrue(playlists.Any());

        }

        [TestMethod]
        public void UpdatePlaylistTest()
        {
            Assert.IsTrue(Client.Playlists.UpdatePlaylist(listId, name: "renamedTestpl", songIdsToRemove:new []{3617}));
        }

        [TestMethod]
        public void GetPlaylistTest()
        {
            Assert.IsTrue(Client.Playlists.GetPlaylist(listId).Items.Any());
        }

        [TestMethod]
        public void DeletePlaylistTest()
        {
            Assert.IsTrue(Client.Playlists.DeletePlaylist(listId));
        }

    }
}