using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp
{
    public class SubsonicClient
    {
        public UserToken User { get; }
        public ServerInfo Server { get; }
        public string ClientName { get; } = "SubSharp"; //Required field for requests

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
            return
                $"{Server.BaseUrl()}/rest/{command.MethodName}?{command.ParameterString()}{User}&{Server.VersionString()}&c={ClientName}";
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

        public XDocument GetResponseXDocument(RestCommand command)
        {
            string text = GetResponseTask(command).Result;
            return XDocument.Parse(text);
        }

        #region System

        //Return true on successful ping
        public bool PingServer()
        {
            RestCommand ping = new RestCommand { MethodName = "ping" };
            XDocument document = GetResponseXDocument(ping);
            return document.Root.Attribute("status").Value.Equals("ok");
        }

        public License GetLicense()
        {
            RestCommand licenseCommand = new RestCommand { MethodName = "getLicense" };
            XDocument document = GetResponseXDocument(licenseCommand);
            XElement licenseElement = document.Root.Descendants().First();
            string valid = licenseElement.Attribute("valid").Value;
            string email = licenseElement.Attribute("email").Value;
            string expires = licenseElement.LastAttribute.Value;
            //Accessed without name because it varies between "licenseExpires" and "trialExpires"
            return new License(valid, email, expires);
        }

        #endregion System

        #region Browsing

        public IEnumerable<BasicItem> GetMusicFolders()
        {
            RestCommand command = new RestCommand { MethodName = "getMusicFolders" };
            XDocument document = GetResponseXDocument(command);
            XElement parentElement = document.Root.Descendants().First();
            foreach (XElement element in parentElement.Descendants())
            {
                Debug.WriteLine(element.Attribute("id").Value);
                Debug.WriteLine(element.Attribute("name").Value);
                yield return
                    new BasicItem
                    {
                        Id = int.Parse(element.Attribute("id").Value),
                        Kind = ItemType.MusicFolder,
                        Name = element.Attribute("name").Value
                    };
            }
        }


        //todo: add support for parameters
        public IndexesCollection GetIndexes()
        {
            RestCommand command = new RestCommand { MethodName = "getIndexes" };
            XDocument document = GetResponseXDocument(command);
            IndexesCollection collection = IndexesCollection.Create(document);
            return collection;
        }

        public Directory GetMusicDirectory(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getMusicDirectory",
                Parameters = { new RestParameter { Parameter = "id", Value = id.ToString() } }
            };
            XDocument document = GetResponseXDocument(command);
            Directory directory = Directory.Create(document);
            return directory;
        }

        public IEnumerable<Genre> GetGenres()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getGenres"
            };
            XDocument document = GetResponseXDocument(command);
            return
                document
                    .Root
                    .Elements()
                    .First().Elements()
                    .Where(x => x.Name.LocalName == "genre")
                    .Select(Genre.Create);
        }

        //todo: add parameter support
        public Dictionary<string, IEnumerable<Artist>> GetArtists()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtists"
            };
            XDocument document = GetResponseXDocument(command);
            Dictionary<string, IEnumerable<Artist>> dict = new Dictionary<string, IEnumerable<Artist>>();
            foreach (XElement index in document.Root.Elements().First().Elements().Where(x => x.Name.LocalName == "index"))
            {
                dict.Add(index.Attribute("name").Value, index.EnumerateArtists());
            }
            return dict;
        }

        #endregion Browsing
    }
}