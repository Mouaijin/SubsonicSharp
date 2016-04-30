using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xBrainLab.Security.Cryptography;

namespace SubsonicSharp
{
    class UserToken
    {
        public string Username { get; set; }
        public bool Plaintext { get; set; }
        public string Token { get; set; }
        public string Salt { get; set; }


        public UserToken(string username, string password, bool plaintext = false, int saltLength = 6)
        {
            Plaintext = plaintext;
            Username = username;
            if (Plaintext)
            {
                Token = password;
            }
            else
            {
                Salt = GenerateSalt(saltLength);
                Token = GenerateMd5(password + Salt);
            }
        }

        //Generates the MD5 hash for a given string
        internal static string GenerateMd5(string text)
        {
            string hash = MD5.GetHashString(text);
            //Subsonic expects letters to be lowercase
            return hash.ToLower();
        }

        //Generates the salt needed for hashed password
        internal static string GenerateSalt(int length)
        {
            const string valid = "0123456789abcdefghijklmnopqrstuvwxyz";

            //As this token is sent in clear text anyway, it hardly seems worth using a cryptographic RNG with a 300x performance hit
            Random rand = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = valid[rand.Next(valid.Length)];
            }
            return new string(chars);
        }

        internal void UpdatePassword(string password)
        {
            Token = GenerateMd5(password + Salt);
        }

        public override string ToString()
        {
            if (Plaintext)
                return $"u={Username}&p={Token}";
            else
            {
                return $"u={Username}&t={Token}&s={Salt}";
            }
        }
    }
}
