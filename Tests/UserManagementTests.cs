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
    public class UserManagementTests
    {
        public SubsonicClient Client { get; set; }

        public UserManagementTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }

        [TestMethod]
        public void GetUserTest()
        {
            User user = Client.UserManagement.GetUser("test");
            Assert.AreEqual("test", user.Username);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            IEnumerable<User> users = Client.UserManagement.GetUsers();
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void CreateUserTest()
        {
            Assert.IsTrue(Client.UserManagement.CreateUser("test2","test2","email@email.com"));
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            Assert.IsTrue(Client.UserManagement.UpdateUser("test2", email: "changed@email.com"));
            Assert.IsTrue(Client.UserManagement.GetUser("test2").Email == "changed@email.com");
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            Assert.IsTrue(Client.UserManagement.ChangePassword("test2", "test3"));
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            Assert.IsTrue(Client.UserManagement.DeleteUser("test2"));
        }

    }
}