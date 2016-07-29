using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.ActionGroups;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class InformationListsTests
    {
        public SubsonicClient Client { get; }
        public InformationListsTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void GetAlbumListTest()
        {
            IEnumerable<Album> albums = Client.InformationLists.GetAlbumList(ListOrdering.AlphabeticalByArtist);
            Assert.IsTrue(albums.Any());
        }

        [TestMethod]
        public void GetAlbumList2Test()
        {
            IEnumerable<Album> albums = Client.InformationLists.GetAlbumList2(ListOrdering.Random, musicFolderId: 0,
                toYear: 2007);
            Assert.IsTrue(albums.Any());
        }

        [TestMethod]
        public void GetRandomSongsTest()
        {
            IEnumerable<Child> songs = Client.InformationLists.GetRandomSongs(size: 100);
            Assert.IsTrue(songs.Any());
        }

        [TestMethod]
        public void GetSongsByGenreTest()
        {
            IEnumerable<Child> songs = Client.InformationLists.GetSongsByGenre("Rock", count: 500);
            Assert.IsTrue(songs.Any());
        }

        [TestMethod]
        public void GetNowPlayingTest()
        {
            IEnumerable<NowPlaying> playing = Client.InformationLists.GetNowPlaying();
            Assert.IsTrue(playing.Any());
        }

        [TestMethod]
        public void GetStarredTest()
        {
            SearchResult starred = Client.InformationLists.GetStarred();
            Assert.IsTrue(starred.Albums.Any() || starred.Artists.Any() || starred.Items.Any());
        }
        [TestMethod]
        public void GetStarred2Test()
        {
            SearchResult starred = Client.InformationLists.GetStarred2(0);
            Assert.IsTrue(starred.Albums.Any() || starred.Artists.Any() || starred.Items.Any());
        }
    }
}
