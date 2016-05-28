using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class ClientBrowser
    {
        public SubsonicClient Client { get; private set; }

        internal ClientBrowser(SubsonicClient client)
        {
            Client = client;
        }

        #region Public Methods

        public IEnumerable<BasicItem> GetMusicFolders()
        {
            RestCommand command = new RestCommand { MethodName = "getMusicFolders" };
            XDocument document = Client.GetResponseXDocument(command);
            return GetMusicFolders(document);
        }

        public IndexesCollection GetIndexes()
        {
            RestCommand command = new RestCommand { MethodName = "getIndexes" };
            XDocument document = Client.GetResponseXDocument(command);
            return GetIndexes(document);
        }

        public Directory GetMusicDirectory(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getMusicDirectory",
                Parameters = { new RestParameter { Parameter = "id", Value = id.ToString() } }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetMusicDirectory(document);
        }

        public IEnumerable<Genre> GetGenres()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getGenres"
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetGenres(document);
        }

        public Dictionary<string, IEnumerable<Artist>> GetArtists()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtists"
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetArtists(document);
        }

        public Artist GetArtist(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtist",
                Parameters = { new RestParameter { Parameter = "id", Value = id.ToString() } }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetArtist(document);
        }

        public Album GetAlbum(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getAlbum",
                Parameters = { new RestParameter { Parameter = "id", Value = id.ToString() } }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetAlbum(document);
        }

        public Child GetSong(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getSong",
                Parameters = { new RestParameter { Parameter = "id", Value = id.ToString() } }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetSong(document);
        }

        public IEnumerable<Child> GetVideos()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getVideos"
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetVideos(document);
        }

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
            XDocument document = Client.GetResponseXDocument(command);
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
            XDocument document = Client.GetResponseXDocument(command);
            return GetArtistInfo(document);
        }

        #endregion Public Methods

        #region Internal Methods

        internal IEnumerable<BasicItem> GetMusicFolders(XDocument document)
        {
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
        internal IndexesCollection GetIndexes(XDocument document)
        {
            IndexesCollection collection = IndexesCollection.Create(document);
            return collection;
        }

        internal Directory GetMusicDirectory(XDocument document) => Directory.Create(document);

        internal IEnumerable<Genre> GetGenres(XDocument document)
        {
            return
                document
                    .Root
                    .Elements()
                    .First().Elements()
                    .Where(x => x.Name.LocalName == "genre")
                    .Select(Genre.Create);
        }

        //todo: add parameter support
        internal Dictionary<string, IEnumerable<Artist>> GetArtists(XDocument document)
        {
            Dictionary<string, IEnumerable<Artist>> dict = new Dictionary<string, IEnumerable<Artist>>();
            foreach (
                XElement index in document.Root.Elements().First().Elements().Where(x => x.Name.LocalName == "index"))
            {
                dict.Add(index.Attribute("name").Value, index.EnumerateArtists());
            }
            return dict;
        }

        internal Artist GetArtist(XDocument document) => Artist.Create(document.Root.Elements().First());
        internal Album GetAlbum(XDocument document) => Album.Create(document.Root.Elements().First());

        internal Child GetSong(XDocument document) => Child.Create(document.Root.Elements().First());


        internal IEnumerable<Child> GetVideos(XDocument document)
            => document.Root.Elements().First().EnumerateChildren();


        internal ArtistInfo GetArtistInfo(XDocument document) => ArtistInfo.Create(document.Root.Elements().First());

        #endregion Internal Methods
    }
}