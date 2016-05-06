using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;

namespace Tests
{
    [TestClass]
    public class ConnectionTests
    {
        public SubsonicClient Client { get; set; }
        public ConnectionTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
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
            string expected = "http://192.168.1.140:4040/rest/ping?u=test&p=test&c=SubSharp";
            string actual = Client.FormatCommand(pingCommand);
            Assert.AreEqual(expected,actual);
        }
    }
}