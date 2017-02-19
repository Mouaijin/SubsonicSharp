using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;

namespace Tests
{
    [TestClass]
    public class MediaAnnotationTests
    {

        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }
        public MediaAnnotationTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void StarTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.MediaAnnotation.Star(new []{999}));
            Assert.IsTrue(Client.MediaAnnotation.StarAlbums(new []{634}));
            Assert.IsTrue(Client.MediaAnnotation.StarArtists(new []{170}));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void UnstarTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.MediaAnnotation.Unstar(new[] { 999 }));
            Assert.IsTrue(Client.MediaAnnotation.UnstarAlbums(new[] { 634 }));
            Assert.IsTrue(Client.MediaAnnotation.UnstarArtists(new[] { 170 }));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void SetRatingTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.MediaAnnotation.SetRating(999, 5));
            Assert.IsTrue(Client.MediaAnnotation.SetRating(999, 0));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}
