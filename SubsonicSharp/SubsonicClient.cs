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
        public string ClientName { get; set; } = "SubSharp"; //Required field for requests, 

        public SubsonicClient(string username, string password, string address, int port = 4040)
        {
            User = new UserToken(username, password);
            Server = new ServerInfo(address, port);
        }

        public SubsonicClient(UserToken user, ServerInfo server)
        {
            User = user;
            Server = server;
        }

        public string FormatCommand(RestCommand command)
        {
            return $"{Server.BaseUrl()}/rest/{command.MethodName}?{command.ParameterString()}{User}&c={ClientName}";
        }

        //Return true on successful ping
        //public bool PingServer()
        //{
            
        //}

    }
}
