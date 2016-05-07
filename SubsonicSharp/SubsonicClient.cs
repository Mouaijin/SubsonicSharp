﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            return $"{Server.BaseUrl()}/rest/{command.MethodName}?{command.ParameterString()}{User}&{Server.VersionString()}&c={ClientName}";
        }

        private async Task<string> GetResponseTask(RestCommand command)
        {
            string url = FormatCommand(command);
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(uri))
            using (HttpContent content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        public Stream GetResponseStream(RestCommand command)
        {
            string text = GetResponseTask(command).Result;
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        //Return true on successful ping
        //public bool PingServer()
        //{
            
        //}

    }
}
