using System.Net.Mime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Tests
{
    [TestClass]
    public class MediaTests
    {
        public SubsonicClient Client { get; set; }

        public MediaTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void GetStreamingUrlTest()
        {
            string actual = Client.MediaRetrieval.GetStreamingAddress(999, 0, "raw", true);
            string expected =
                "http://192.168.1.140:4040/rest/stream?id=999&format=raw&estimateContentLength=True&u=test&p=test&v=1.13&c=SubSharp";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDownloadUrlTest()
        {
            string actual = Client.MediaRetrieval.GetDownloadAddress(999);
            string expected = "http://192.168.1.140:4040/rest/download?id=999&u=test&p=test&v=1.13&c=SubSharp";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCoverArtTest()
        {
            Client.MediaRetrieval.GetCovertArtAsync(999).ContinueWith(result =>
            {
                using (var img = Image.FromStream(new MemoryStream(result.Result)))
                {
                    img.Save("C:\\Users\\thechr\\Desktop\\testing.jpg", ImageFormat.Jpeg);
                }
            });
        }
    }
}