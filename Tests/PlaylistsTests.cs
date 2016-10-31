using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class PlaylistsTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }
        private int listId;
        public PlaylistsTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void CreatePlaylistTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Playlists.CreatePlaylist("testpl", new []{2617,999}));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetPlaylistsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<Playlist> playlists = Client.Playlists.GetPlaylists(Username);
            var enumerable = playlists as IList<Playlist> ?? playlists.ToList();
            listId = enumerable.First().Id;
            Assert.IsTrue(enumerable.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void UpdatePlaylistTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Playlists.UpdatePlaylist(listId, name: "renamedTestpl", songIdsToRemove:new []{3617}));
            
            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetPlaylistTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Playlists.GetPlaylist(listId).Items.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void DeletePlaylistTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Playlists.DeletePlaylist(listId));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

    }
}