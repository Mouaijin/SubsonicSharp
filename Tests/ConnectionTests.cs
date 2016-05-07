using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
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
            string expected = "http://192.168.1.140:4040/rest/ping?u=test&p=test&v=1.13&c=SubSharp";
            string actual = Client.FormatCommand(pingCommand);
            Assert.AreEqual(expected,actual);
            //Stream response = Client.GetResponseStream(pingCommand);
            //XmlReader reader = XmlReader.Create(response);
            //string res = "";
            //while (reader.Read())
            //{
            //    res += reader.LocalName + reader.Value;
            //}
            //Debug.WriteLine(res);
        }
    }
}