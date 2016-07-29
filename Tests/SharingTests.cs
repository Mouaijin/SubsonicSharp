using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class SharingTests
    {
        public SubsonicClient Client { get; }
        private int shareId;

        public SharingTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void CreateShareTest()
        {
           Share share = Client.Sharing.CreateShare(new[] {3617, 999}, "description", DateTime.Today);
            Assert.AreEqual("description", share.Description);
            shareId = share.Id;
        }

        [TestMethod]
        public void UpdateShareTest()
        {
            bool success = Client.Sharing.UpdateShare(shareId, "newDescription", DateTime.Today.AddDays(1));
        }

        [TestMethod]
        public void GetSharesTest()
        {
            IEnumerable<Share> shares = Client.Sharing.GetShares();
            Assert.AreEqual("newDescription", shares.First().Description);
            Assert.AreEqual(shareId, shares.First().Id);
            Assert.AreEqual(DateTime.Today.AddDays(1), shares.First().Expires);
        }

        [TestMethod]
        public void DeleteShareTest()
        {
        }
    }
}