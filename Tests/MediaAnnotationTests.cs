using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;

namespace Tests
{
    [TestClass]
    public class MediaAnnotationTests
    {
        public SubsonicClient Client { get; }
        public MediaAnnotationTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void StarTest()
        {
            Assert.IsTrue(Client.MediaAnnotation.Star(new []{999}));
            Assert.IsTrue(Client.MediaAnnotation.StarAlbums(new []{634}));
            Assert.IsTrue(Client.MediaAnnotation.StarArtists(new []{170}));
        }

        [TestMethod]
        public void UnstarTest()
        {
            Assert.IsTrue(Client.MediaAnnotation.Unstar(new[] { 999 }));
            Assert.IsTrue(Client.MediaAnnotation.UnstarAlbums(new[] { 634 }));
            Assert.IsTrue(Client.MediaAnnotation.UnstarArtists(new[] { 170 }));
        }

        [TestMethod]
        public void SetRatingTest()
        {
            Assert.IsTrue(Client.MediaAnnotation.SetRating(999, 5));
            Assert.IsTrue(Client.MediaAnnotation.SetRating(999, 0));
        }
    }
}
