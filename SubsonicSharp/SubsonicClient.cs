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
        //todo: Make public counter parts that do the calls and pass in the Xdoc parameters, and make these calls internal
        public IEnumerable<BasicItem> GetMusicFolders(XDocument document = null)
        {
            if (document == null)
            {
                RestCommand command = new RestCommand {MethodName = "getMusicFolders"};
                document = GetResponseXDocument(command);
            }
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
        public IndexesCollection GetIndexes(XDocument document = null)
        {
            if (document == null)
            {
                RestCommand command = new RestCommand {MethodName = "getIndexes"};
                document = GetResponseXDocument(command);
            }
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

        public Directory GetMusicDirectory(XDocument document) => Directory.Create(document);


        public IEnumerable<Genre> GetGenres(XDocument document = null)
        {
            if (document == null)
            {
                RestCommand command = new RestCommand
                {
                    MethodName = "getGenres"
                };
                document = GetResponseXDocument(command);
            }
            return
                document
                    .Root
                    .Elements()
                    .First().Elements()
                    .Where(x => x.Name.LocalName == "genre")
                    .Select(Genre.Create);
        }

        //todo: add parameter support
        public Dictionary<string, IEnumerable<Artist>> GetArtists(XDocument document = null)
        {
            if (document == null)
            {
                RestCommand command = new RestCommand
                {
                    MethodName = "getArtists"
                };
                document = GetResponseXDocument(command);
            }
            Dictionary<string, IEnumerable<Artist>> dict = new Dictionary<string, IEnumerable<Artist>>();
            foreach (XElement index in document.Root.Elements().First().Elements().Where(x => x.Name.LocalName == "index"))
            {
                dict.Add(index.Attribute("name").Value, index.EnumerateArtists());
            }
            return dict;
        }

        public Artist GetArtist(XDocument document) => Artist.Create(document.Root.Elements().First());

        public Artist GetArtist(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtist",
                Parameters = {new RestParameter { Parameter = "id", Value = id.ToString()} }
            };
            XDocument document = GetResponseXDocument(command);
            return GetArtist(document);
        }

        public Album GetAlbum(XDocument document) => Album.Create(document.Root.Elements().First());

        public Album GetAlbum(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getAlbum",
                Parameters = {new RestParameter {Parameter = "id", Value = id.ToString()}}
            };
            XDocument document = GetResponseXDocument(command);
            return GetAlbum(document);
        }

        public Child GetSong(XDocument document) => Child.Create(document.Root.Elements().First());

        public Child GetSong(int id)
        {
            RestCommand command = new RestCommand
            { MethodName = "getSong",
            Parameters = {new RestParameter { Parameter = "id", Value = id.ToString()} }
            };
            XDocument document = GetResponseXDocument(command);
            return GetSong(document);
        }

        public IEnumerable<Child> GetVideos(XDocument document) => document.Root.Elements().First().EnumerateChildren();

        public IEnumerable<Child> GetVideos()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getVideos"
            };
            XDocument document = GetResponseXDocument(command);
            return GetVideos(document);
        }

        public ArtistInfo GetArtistInfo(XDocument document) => ArtistInfo.Create(document.Root.Elements().First());

        public ArtistInfo GetArtistInfo(int id, int count = 20, bool includeNotPresent = false)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtistInfo",
                Parameters =
                {
                    new RestParameter {Parameter = "id", Value = id.ToString()},
                    new RestParameter {Parameter = "count", Value = count.ToString()},
                    new RestParameter {Parameter = "includeNotPresent", Value = includeNotPresent.ToString()}
                }

            };
            XDocument document = GetResponseXDocument(command);
            return GetArtistInfo(document);
        }
        public ArtistInfo GetArtistInfo2(int id, int count = 20, bool includeNotPresent = false)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtistInfo2",
                Parameters =
                {
                    new RestParameter {Parameter = "id", Value = id.ToString()},
                    new RestParameter {Parameter = "count", Value = count.ToString()},
                    new RestParameter {Parameter = "includeNotPresent", Value = includeNotPresent.ToString()}
                }

            };
            XDocument document = GetResponseXDocument(command);
            return GetArtistInfo(document);
        }
        #endregion Browsing
    }
}