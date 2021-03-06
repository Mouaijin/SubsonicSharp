﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        /// <summary>
        /// Returns all configured top-level music folders. Takes no extra parameters.
        /// </summary>
        /// <returns>A collection of BasicItem objects representing names and IDs of all music folders</returns>
        [ApiLevel(0)]
        public IEnumerable<BasicItem> GetMusicFolders()
        {
            RestCommand command = new RestCommand {MethodName = "getMusicFolders"};
            XDocument document = Client.GetResponseXDocument(command);
            return GetMusicFolders(document);
        }

        /// <summary>
        /// Returns an indexed structure of all artists.
        /// </summary>
        /// <returns>An IndexesCollection object with information about all indexes</returns>
        [ApiLevel(0)]
        public IndexesCollection GetIndexes()
        {
            RestCommand command = new RestCommand {MethodName = "getIndexes"};
            XDocument document = Client.GetResponseXDocument(command);
            return GetIndexes(document);
        }

        /// <summary>
        /// Returns a listing of all files in a music directory. Typically used to get list of albums for an artist, or list of songs for an album.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Directory object with information about the requested directory</returns>
        [ApiLevel(0)]
        public Directory GetMusicDirectory(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getMusicDirectory",
                Parameters = {new RestParameter {Parameter = "id", Value = id.ToString()}}
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetMusicDirectory(document);
        }

        /// <summary>
        /// Returns all genres. 
        /// </summary>
        /// <returns>A collection of Genre objects for all genres in system</returns>
        [ApiLevel(9)]
        public IEnumerable<Genre> GetGenres()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getGenres"
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetGenres(document);
        }

        /// <summary>
        /// Similar to getIndexes, but organizes music according to ID3 tags. 
        /// </summary>
        /// <returns>A Dictionary of Artist objects organized by alphabetic index</returns>
        [ApiLevel(8)]
        public Dictionary<string, IEnumerable<Artist>> GetArtists(int musicFolderId = -1)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtists"
            };
            if(musicFolderId != -1) command.AddParameter("musicFolderId", musicFolderId);
            XDocument document = Client.GetResponseXDocument(command);
            return GetArtists(document);
        }

        /// <summary>
        /// Same as GetArtists, but as an ungrouped collection
        /// </summary>
        /// <returns>An enumerable collection of all Artists</returns>
        [ApiLevel(8)]
        public IEnumerable<Artist> GetArtistsUngrouped(int musicFolderId = -1)
        {
            foreach (KeyValuePair<string, IEnumerable<Artist>> pair in GetArtists(musicFolderId))
            {
                foreach (Artist artist in pair.Value)
                {
                    yield return artist;
                }
            }
        }

        /// <summary>
        /// Returns details for an artist, including a list of albums. This method organizes music according to ID3 tags. 
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <returns>An Artist object for the requested artist</returns>
        [ApiLevel(8)]
        public Artist GetArtist(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getArtist",
                Parameters = {new RestParameter {Parameter = "id", Value = id.ToString()}}
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetArtist(document);
        }

        /// <summary>
        /// Returns a collection of all albums on the server, or in a specific folder
        /// </summary>
        /// <param name="musicFolderId">The directory to get albums for. Leave blank for all folders.</param>
        /// <returns>A collection of Album objects</returns>
        [ApiLevel(2)]
        public IEnumerable<Album> GetAllAlbums(int musicFolderId = -1)
        {
            int offset = 0;
            bool empty = false;
            while (!empty)
            {

                IEnumerable<Album> albums = Client.InformationLists.GetAlbumList(ListOrdering.AlphabeticalByName, 500,
                    offset, musicFolderId: musicFolderId);
                if (!albums.Any())
                    empty = true;
                else
                {
                    foreach (Album album in albums)
                    {
                        yield return album;
                    }
                    offset += 500;
                }
            }

        }

        /// <summary>
        /// Returns details for an album, including a list of songs. This method organizes music according to ID3 tags. 
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <returns>An Album object for the request album</returns>
        [ApiLevel(8)]
        public Album GetAlbum(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getAlbum",
                Parameters = {new RestParameter {Parameter = "id", Value = id.ToString()}}
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetAlbum(document);
        }

        /// <summary>
        /// Returns details for a song. 
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <returns>A Child object for the requested song</returns>
        [ApiLevel(8)]
        public Child GetSong(int id)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getSong",
                Parameters = {new RestParameter {Parameter = "id", Value = id.ToString()}}
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetSong(document);
        }

        /// <summary>
        /// Returns all video files. 
        /// </summary>
        /// <returns>A collection of Child objects for all videos in system</returns>
        [ApiLevel(8)]
        public IEnumerable<Child> GetVideos()
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getVideos"
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetVideos(document);
        }

        /// <summary>
        /// Returns album notes, image URLs etc, using data from last.fm.
        /// </summary>
        /// <param name="id">The album or song ID.</param>
        /// <returns>An AlbumInfo object with data from last.fm</returns>
        [ApiLevel(14)]
        public AlbumInfo GetAlbumInfo(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getAlbumInfo";
            command.AddParameter("id", id);
            return AlbumInfo.Create(Client.GetResponseXDocument(command).RealRoot());
        }
        /// <summary>
        /// Returns album notes, image URLs etc, using data from last.fm, organized by ID3 tags.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <returns>An AlbumInfo object with data from last.fm</returns>
        [ApiLevel(14)]
        public AlbumInfo GetAlbumInfo2(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getAlbumInfo";
            command.AddParameter("id", id);
            return AlbumInfo.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        /// <summary>
        /// Returns details for a video, including information about available audio tracks, subtitles (captions) and conversions. 
        /// </summary>
        /// <param name="id">The video ID.</param>
        /// <returns>A VideoInfo object with information about the requested video</returns>
        [ApiLevel(14)]
        public VideoInfo GetVideoInfo(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getVideoInfo";
            command.AddParameter("id", id);
            return VideoInfo.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        /// <summary>
        /// Returns artist info with biography, image URLs and similar artists, using data from last.fm. 
        /// </summary>
        /// <param name="id">The artist, album or song ID.</param>
        /// <param name="count">Max number of similar artists to return.</param>
        /// <param name="includeNotPresent">Whether to return artists that are not present in the media library.</param>
        /// <returns>An ArtistInfo object for the specified artist</returns>
        [ApiLevel(11)]
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

        /// <summary>
        ///Similar to getArtistInfo, but organizes music according to ID3 tags.  
        /// </summary>
        /// <param name="id">The artist, album or song ID.</param>
        /// <param name="count">Max number of similar artists to return.</param>
        /// <param name="includeNotPresent">Whether to return artists that are not present in the media library.</param>
        /// <returns>An ArtistInfo object for the specified artist</returns>
        [ApiLevel(11)]
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

        /// <summary>
        ///Returns a random collection of songs from the given artist and similar artists, using data from last.fm. Typically used for artist radio features.  
        /// </summary>
        /// <param name="id">The artist, album or song ID.</param>
        /// <param name="count">Max number of songs to return.</param>
        /// <returns>A collection of Child objects for similar songs</returns>
        [ApiLevel(11)]
        public IEnumerable<Child> GetSimilarSongs(int id, int count = 50)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getSimilarSongs",
                Parameters =
                {
                    new RestParameter {Parameter = "id", Value = id.ToString()},
                    new RestParameter {Parameter = "count", Value = count.ToString()}
                }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetSimilarSongs(document);
        }

        /// <summary>
        ///Similar to getSimilarSongs, but organizes music according to ID3 tags.  
        /// </summary>
        /// <param name="id">The artist, album or song ID.</param>
        /// <param name="count">Max number of songs to return.</param>
        /// <returns>A collection of Child objects for similar songs</returns>
        [ApiLevel(11)]
        public IEnumerable<Child> GetSimilarSongs2(int id, int count = 50)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getSimilarSongs2",
                Parameters =
                {
                    new RestParameter {Parameter = "id", Value = id.ToString()},
                    new RestParameter {Parameter = "count", Value = count.ToString()}
                }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetSimilarSongs(document);
        }

        /// <summary>
        ///Returns top songs for the given artist, using data from last.fm.  
        /// </summary>
        /// <param name="artistName">The artist name.</param>
        /// <param name="count">Max number of songs to return.</param>
        /// <returns>A collection of Child objects representing the top songs</returns>
        [ApiLevel(13)]
        public IEnumerable<Child> GetTopSongs(string artistName, int count = 50)
        {
            RestCommand command = new RestCommand
            {
                MethodName = "getTopSongs",
                Parameters =
                {
                    new RestParameter {Parameter = "artist", Value = artistName},
                    new RestParameter {Parameter = "count", Value = count.ToString()}
                }
            };
            XDocument document = Client.GetResponseXDocument(command);
            return GetTopSongs(document);
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
                XElement index in document.RealRoot().Elements().Where(x => x.Name.LocalName == "index"))
            {
                dict.Add(index.Attribute("name").Value, index.EnumerateArtists());
            }
            return dict;
        }

        internal Artist GetArtist(XDocument document) => Artist.Create(document.RealRoot());
        internal Album GetAlbum(XDocument document) => Album.Create(document.RealRoot());

        internal Child GetSong(XDocument document) => Child.Create(document.RealRoot());


        internal IEnumerable<Child> GetVideos(XDocument document)
            => document.RealRoot().EnumerateChildren();


        internal ArtistInfo GetArtistInfo(XDocument document) => ArtistInfo.Create(document.RealRoot());

        internal IEnumerable<Child> GetSimilarSongs(XDocument document)
            => document.RealRoot().EnumerateChildren();

        internal IEnumerable<Child> GetTopSongs(XDocument document)
            => document.RealRoot().EnumerateChildren();

        #endregion Internal Methods
    }
}