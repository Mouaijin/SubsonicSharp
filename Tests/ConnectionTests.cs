using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;
using System.Net;

namespace Tests
{
    [TestClass]
    public class ConnectionTests
    {
        public SubsonicClient Client { get; }
        public ConnectionTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140", 4041, 13, "", ServerInfo.Protocol.Https);
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
            string expected = "https://192.168.1.140:4041/rest/ping?u=test&p=test&v=1.13&c=SubSharp";
            string actual = Client.FormatCommand(pingCommand);
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void PingTest()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            bool response = Client.PingServer();
            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void LicenseTest()
        {
            License license = Client.GetLicense();
            Assert.AreEqual(true, license.Valid);
            Assert.AreEqual("vypon3000@gmail.com", license.Email);
            //DateTime seems to be returned in UTC format- DateTime.Parse should be okay with this
            DateTime expected = new DateTime(2017, 6, 14, 10, 5, 6, 578);
            Assert.AreEqual(expected, license.Expires);
        }
    }
}