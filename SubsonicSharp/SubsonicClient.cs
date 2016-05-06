using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubsonicSharp
{
    public class SubsonicClient
    {
        public UserToken User { get; set; }
        public ServerInfo Server { get; set; }

        public SubsonicClient(string username, string password, string address, int port = 4040)
        {
            User = new UserToken(username, password);
            Server = new ServerInfo(address, port);
        }
    }
}
