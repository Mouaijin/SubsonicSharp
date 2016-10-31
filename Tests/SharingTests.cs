using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class SharingTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }
        private int shareId;

        public SharingTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void CreateShareTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Share share = Client.Sharing.CreateShare(new[] {3617, 999}, "description", DateTime.Today);
            Assert.AreEqual("description", share.Description);
            shareId = share.Id;

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void UpdateShareTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            bool success = Client.Sharing.UpdateShare(shareId, "newDescription", DateTime.Today.AddDays(1));
            Assert.IsTrue(success);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetSharesTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<Share> shares = Client.Sharing.GetShares();
            Assert.AreEqual("newDescription", shares.First().Description);
            Assert.AreEqual(shareId, shares.First().Id);
            Assert.AreEqual(DateTime.Today.AddDays(1), shares.First().Expires);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void DeleteShareTest()
        {
        }
    }
}