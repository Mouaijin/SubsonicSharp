using System.Net.Mime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class MediaTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; set; }

        public MediaTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void GetStreamingUrlTest()
        {
            string actual = Client.MediaRetrieval.GetStreamingAddress(999, 0, "raw", true);
            string expected =
                $"http://{Hostname}:4040/rest/stream?id=999&format=raw&estimateContentLength=True&u={Username}&p={Password}&v=1.13&c=SubSharp";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDownloadUrlTest()
        {
            string actual = Client.MediaRetrieval.GetDownloadAddress(999);
            string expected = $"http://{Hostname}:4040/rest/download?id=999&u={Username}&p={Password}&v=1.13&c=SubSharp";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCoverArtTest()
        {
            byte[] bytes = Client.MediaRetrieval.GetCovertArt(999);
            File.WriteAllBytes("C:\\Users\\thech\\Desktop\\testing.jpg", bytes);
        }

        [TestMethod]
        public void GetLyricsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            string artist = "Muse";
            string title = "Hysteria";
            Lyrics actual = Client.MediaRetrieval.GetLyrics(artist, title);
            string expectedText =
                "It\'s bugging me";
            Assert.AreEqual(artist, actual.Artist);
            Assert.AreEqual(title, actual.Title);
            Assert.IsTrue(actual.Text.StartsWith(expectedText));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}