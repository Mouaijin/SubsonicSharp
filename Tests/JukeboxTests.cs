using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;

namespace Tests
{
    [TestClass]
    public class JukeboxTests
    {
        public SubsonicClient Client { get; }
        public JukeboxTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void JukeboxAddTest()
        {
            Assert.IsNotNull(Client.JukeboxAdd(999));
            Assert.IsNotNull(Client.JukeboxAdd(7435));
        }

        [TestMethod]
        public void JukeboxSetTest()
        {
            Assert.IsNotNull(Client.JukeboxSet(1));
        }

        [TestMethod]
        public void JukeboxStatsTest()
        {
            Assert.IsFalse(Client.JukeboxStats().Playing);
        }

        [TestMethod]
        public void JukeboxShuffleTest()
        {
            Assert.IsNotNull(Client.JukeboxShuffle());
        }

        [TestMethod]
        public void JukeboxSetGainTest()
        {
            Assert.IsNotNull(Client.JukeboxSetGain(.5f));
        }
        [TestMethod]
        public void JukeboxStartTest()
        {
            Assert.IsTrue(Client.JukeboxStart().Playing);
        }

        [TestMethod]
        public void JukeboxSkipTest()
        {
            Assert.IsNotNull(Client.JukeboxSkip(offset:30));
        }

        [TestMethod]
        public void JukeboxStopTest()
        {
            Assert.IsFalse(Client.JukeboxStop().Playing);
        }

        [TestMethod]
        public void JukeboxRemoveTest()
        {
            Assert.IsNotNull(Client.JukeboxRemove(1));
        }

        [TestMethod]
        public void JukeboxClearTest()
        {
            Client.JukeboxClear();
            Assert.IsFalse(Client.GetJukeboxPlaylist().Items.Any());
        }
    }
}
