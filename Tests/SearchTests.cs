using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class SearchTests
    {
        public SubsonicClient Client { get; set; }

        public SearchTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void Search2Test()
        {
            SearchResult result = Client.Search.Search2("Ut*ri");
            Assert.IsTrue(result.Items.Any());
        }

        [TestMethod]
        public void Search3Test()
        {
            SearchResult result = Client.Search.Search3("Shrine*", musicFolderId:0);
            Assert.IsTrue(result.Artists.Any() || result.Albums.Any() || result.Items.Any());
        }

        //todo: Make actually useful tests for searching. 
    }
}
