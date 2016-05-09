using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;

namespace Tests
{
    [TestClass]
    public class BrowsingTests
    {
        public SubsonicClient Client { get; }
        public BrowsingTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void GetMusicFoldersTest()
        {
            IEnumerable<BasicItem> folders = Client.GetMusicFolders();
            List<BasicItem> expectedFolders = new List<BasicItem>
            {
                new BasicItem {Id = 0, Kind = ItemType.MusicFolder, Name = "Music"},
                new BasicItem {Id = 1, Kind = ItemType.MusicFolder, Name = "Mom's Music"}
            };
            int count =0;
            foreach (BasicItem item in folders)
            {
                count++;
                Assert.IsTrue(expectedFolders.Contains(item));
            }
            Assert.AreEqual(2, count);
        }
    }
}
