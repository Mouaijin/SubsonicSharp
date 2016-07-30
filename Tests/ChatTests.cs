using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class ChatTests
    {
        public SubsonicClient Client { get; }

        public ChatTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void AddChatMessageTest()
        {
            Assert.IsTrue(Client.Chat.AddChatMessage("Another message"));
        }

        [TestMethod]
        public void GetChatMessagesTest()
        {
            IEnumerable<ChatMessage> msgs = Client.Chat.GetChatMessages();
            Assert.IsTrue(msgs.Any());
            foreach (ChatMessage msg in msgs)
            {
                Debug.WriteLine(msg.Username + ": " + msg.Message);
            }
        }
    }
}