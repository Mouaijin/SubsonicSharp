using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class ChatTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }

        public ChatTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void AddChatMessageTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            Assert.IsTrue(Client.Chat.AddChatMessage("Another message"));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetChatMessagesTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            IEnumerable<ChatMessage> msgs = Client.Chat.GetChatMessages();
            Assert.IsTrue(msgs.Any());
            foreach (ChatMessage msg in msgs)
            {
                Debug.WriteLine(msg.Username + ": " + msg.Message);
            }

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}