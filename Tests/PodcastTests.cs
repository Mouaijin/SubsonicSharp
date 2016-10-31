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
    public class PodcastTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }
        public PodcastTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void CreatePodcastChannelTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Podcasts.CreatePodcastChannel("http://www.pwop.com/feed.aspx?show=dotnetrocks&filetype=master"));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void DownloadPodcastEpisodeTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Podcasts.DownloadPodcastEpisode(0));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetNewestPodcastsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<PodcastChannel> podcasts = Client.Podcasts.GetNewestPodcasts(5);
            Assert.IsTrue(podcasts.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetPodcastsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<PodcastChannel> podcasts = Client.Podcasts.GetPodcasts();
            Assert.IsTrue(podcasts.Any());
            PodcastChannel podcast = Client.Podcasts.GetPodcast(0);
            Assert.AreEqual(0, podcast.Id);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void DeletePodcastEpisodeTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Podcasts.DeletePodcastEpisode(GetTestPodcast().Episodes.First().Id));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void DeletePodcastChannelTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Podcasts.DeletePodcastChannel(GetTestPodcast().Id));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        private PodcastChannel GetTestPodcast()
        {
            return Client.Podcasts.GetPodcasts().First(x => x.Title == ".NET Rocks!");
        }
    }
}
