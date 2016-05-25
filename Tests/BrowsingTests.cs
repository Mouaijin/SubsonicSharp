using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

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
                new BasicItem {Id = 1, Kind = ItemType.MusicFolder, Name = "Mom's Music"},
                new BasicItem {Id = 2, Kind = ItemType.MusicFolder, Name = "Styer Music"}
            };
            int count =0;
            foreach (BasicItem item in folders)
            {
                count++;
                Assert.IsTrue(expectedFolders.Contains(item));
            }
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void GetIndexesTest()
        {
            XDocument xDoc = XDocument.Load("TestData/indexes_example_1.xml");
            IndexesCollection collection = IndexesCollection.Create(xDoc);
            Assert.AreEqual(2, collection.Shortcuts.Count(), "Bad shorcuts count");
            Assert.AreEqual(2, collection.Indexes.Keys.Count, "Bad dictionary key count");
            Assert.AreEqual("Bob Dylan", collection.Indexes["B"].Single().Name, "Bob Dylan as only artist in 'B' failed");
            Assert.AreEqual(2, collection.Children.Count());
            Assert.IsTrue(collection.Children.Count(x => x.Id == 111) == 1);
        }

        [TestMethod]
        public void GetMusicDirectoryTest()
        {
            XDocument xDoc = XDocument.Load("TestData/directory_example_2.xml");
            Directory dir = Directory.Create(xDoc);
            Assert.AreEqual(2, dir.Children.Count());
            Assert.AreEqual(11, dir.Id);
            Assert.AreEqual("Arrival", dir.Name);
            Assert.AreEqual(1, dir.Parent);
        }

        [TestMethod]
        public void GetGenresTest()
        {
            IEnumerable<Genre> genres = Client.GetGenres();
            Assert.AreEqual("Electronic", genres.First().Name);
            Assert.AreEqual(3798, genres.First().SongCount);
        }
    }
}
