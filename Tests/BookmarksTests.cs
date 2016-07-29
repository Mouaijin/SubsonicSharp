using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class BookmarksTests
    {
        public SubsonicClient Client { get; }
        public BookmarksTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void CreateBookmarkTest()
        {
            bool success = Client.Bookmarks.CreateBookmark(3617, 5000, "bookmark!");
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetBookmarksTest()
        {
            IEnumerable<Bookmark> books = Client.Bookmarks.GetBookmarks();
            Assert.IsTrue(books.Any());
        }

        [TestMethod]
        public void DeleteBookmarkTest()
        {
            Assert.IsTrue(Client.Bookmarks.DeleteBookmark(3617));
        }

        [TestMethod]
        public void GetPlayQueueTest()
        {
            PlayQueue q = Client.Bookmarks.GetPlayQueue();
            Assert.IsTrue(q.Items.Any());
        }

        [TestMethod]
        public void SavePlayQueueTest()
        {
            Assert.IsTrue(Client.Bookmarks.SavePlayQueue(new []{3617, 5000, 999}, 3617, 3000));
        }
    }
}
