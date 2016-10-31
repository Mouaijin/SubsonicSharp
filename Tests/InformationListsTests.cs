using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.ActionGroups;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class InformationListsTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }
        public InformationListsTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void GetAlbumListTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<Album> albums = Client.InformationLists.GetAlbumList(ListOrdering.AlphabeticalByArtist);
            Assert.IsTrue(albums.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetAlbumList2Test()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<Album> albums = Client.InformationLists.GetAlbumList2(ListOrdering.Random, musicFolderId: 0,
                toYear: 2007);
            Assert.IsTrue(albums.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetRandomSongsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<Child> songs = Client.InformationLists.GetRandomSongs(size: 100);
            Assert.IsTrue(songs.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetSongsByGenreTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<Child> songs = Client.InformationLists.GetSongsByGenre("Rock", count: 500);
            Assert.IsTrue(songs.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetNowPlayingTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<NowPlaying> playing = Client.InformationLists.GetNowPlaying();
            Assert.IsTrue(playing.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetStarredTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            SearchResult starred = Client.InformationLists.GetStarred();
            Assert.IsTrue(starred.Albums.Any() || starred.Artists.Any() || starred.Items.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
        [TestMethod]
        public void GetStarred2Test()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            SearchResult starred = Client.InformationLists.GetStarred2(0);
            Assert.IsTrue(starred.Albums.Any() || starred.Artists.Any() || starred.Items.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}
