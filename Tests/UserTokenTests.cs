using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;

namespace Tests
{
    [TestClass]
    public class UserTokenTests
    {
        [TestMethod]
        public void Md5Test()
        {
            string hash = UserToken.GenerateMd5("sesamec19b2d");
            string expected = "26719a1196d2a940705a59634eb18eab";
            Assert.AreEqual(expected,hash);
        }

        [TestMethod]
        public void PlainTextPasswordStringTest()
        {
            UserToken test = new UserToken("test", "password", true);
            string testString = test.ToString();
            string expected = "u=test&p=password";
            Assert.AreEqual(expected, testString);
        }

        [TestMethod]
        public void HashedPasswordStringTest()
        {
            UserToken test = new UserToken("test", "password");
            //Make sure it is at least generating something
            Assert.IsNotNull(test.Salt);
            //Set to a specific salt so it's testable
            test.Salt = "abcdef";
            test.UpdatePassword("password");

            string testString = test.ToString();
            string expected = "u=test&t=9d8d1f345f220fec61ae3ff604416ff6&s=abcdef";
            Assert.AreEqual(expected, testString);
        }
    }
}
