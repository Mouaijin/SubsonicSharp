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
    }
}
