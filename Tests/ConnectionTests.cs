using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class ConnectionTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";
        private const string LicenseEmail = "midwan@gmail.com";
        private readonly DateTime _licenseDate = new DateTime(2017, 05, 24);

        public SubsonicClient Client { get; }
        public ConnectionTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void CreateConnection()
        {
        }

        [TestMethod]
        public void FormatPingCommand()
        {
            RestCommand pingCommand = new RestCommand {MethodName = "ping"};
            string expected = $"http://{Hostname}:4040/rest/ping?u={Username}&p={Password}&v=1.13&c=SubSharp";
            string actual = Client.FormatCommand(pingCommand);
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void PingTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            bool response = Client.PingServer();
            
            Assert.AreEqual(true, response);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void LicenseTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            License license = Client.GetLicense();
            Assert.AreEqual(true, license.Valid);
            Assert.AreEqual(LicenseEmail, license.Email);
            //DateTime seems to be returned in UTC format- DateTime.Parse should be okay with this
            DateTime expected = _licenseDate;
            Assert.AreEqual(expected.Date, license.Expires.Date);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}