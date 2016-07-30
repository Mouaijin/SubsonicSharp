using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class PodcastTests
    {
        public SubsonicClient Client { get; }
        public PodcastTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void CreatePodcastChannelTest()
        {
            Assert.IsTrue(Client.Podcasts.CreatePodcastChannel("http://www.pwop.com/feed.aspx?show=dotnetrocks&filetype=master"));
        }

        [TestMethod]
        public void DownloadPodcastEpisodeTest()
        {
            Assert.IsTrue(Client.Podcasts.DownloadPodcastEpisode(0));
        }

        [TestMethod]
        public void GetNewestPodcastsTest()
        {
            IEnumerable<PodcastChannel> podcasts = Client.Podcasts.GetNewestPodcasts(5);
            Assert.IsTrue(podcasts.Any());
        }

        [TestMethod]
        public void GetPodcastsTest()
        {
            IEnumerable<PodcastChannel> podcasts = Client.Podcasts.GetPodcasts();
            Assert.IsTrue(podcasts.Any());
            PodcastChannel podcast = Client.Podcasts.GetPodcast(0);
            Assert.AreEqual(0, podcast.Id);
        }

        [TestMethod]
        public void DeletePodcastEpisodeTest()
        {
            Assert.IsTrue(Client.Podcasts.DeletePodcastEpisode(GetTestPodcast().Episodes.First().Id));
        }

        [TestMethod]
        public void DeletePodcastChannelTest()
        {
            Assert.IsTrue(Client.Podcasts.DeletePodcastChannel(GetTestPodcast().Id));
        }

        private PodcastChannel GetTestPodcast()
        {
            return Client.Podcasts.GetPodcasts().First(x => x.Title == ".NET Rocks!");
        }
    }
}
