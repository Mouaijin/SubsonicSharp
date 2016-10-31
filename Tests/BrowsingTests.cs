using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class BrowsingTests
    {
        private const string Username = "admin";
        private const string Password = "test";
        private const string Hostname = "192.168.1.39";

        public SubsonicClient Client { get; }
        public BrowsingTests()
        {
            UserToken user = new UserToken(Username, Password, true);
            ServerInfo server = new ServerInfo(Hostname);
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void GetMusicFoldersTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/musicFolders_example_1.xml");
            IEnumerable<BasicItem> folders = Client.Browsing.GetMusicFolders(xDoc);
            List<BasicItem> expectedFolders = new List<BasicItem>
            {
                new BasicItem {Id = 1, Kind = ItemType.MusicFolder, Name = "Music"},
                new BasicItem {Id = 2, Kind = ItemType.MusicFolder, Name = "Movies"},
                new BasicItem {Id = 3, Kind = ItemType.MusicFolder, Name = "Incoming"}
            };
            int count =0;
            foreach (BasicItem item in folders)
            {
                count++;
                Assert.IsTrue(expectedFolders.Contains(item));
            }
            Assert.AreEqual(3, count);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetIndexesTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/indexes_example_1.xml");
            IndexesCollection collection = Client.Browsing.GetIndexes(xDoc);
            Assert.AreEqual(2, collection.Shortcuts.Count(), "Bad shorcuts count");
            Assert.AreEqual(2, collection.Indexes.Keys.Count, "Bad dictionary key count");
            Assert.AreEqual("Bob Dylan", collection.Indexes["B"].Single().Name, "Bob Dylan as only artist in 'B' failed");
            Assert.AreEqual(2, collection.Children.Count());
            Assert.IsTrue(collection.Children.Count(x => x.Id == 111) == 1);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetMusicDirectoryTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/directory_example_2.xml");
            Directory dir = Client.Browsing.GetMusicDirectory(xDoc);
            Assert.AreEqual(2, dir.Children.Count());
            Assert.AreEqual(11, dir.Id);
            Assert.AreEqual("Arrival", dir.Name);
            Assert.AreEqual(1, dir.Parent);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetGenresTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/genres_example_1.xml");
            IEnumerable<Genre> genres = Client.Browsing.GetGenres(xDoc);
            Assert.AreEqual("Electronic", genres.First().Name);
            Assert.AreEqual(28, genres.First().SongCount);
            Assert.AreEqual(7, genres.Count());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetArtistsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc =XDocument.Load("TestData/artists_example_1.xml");
            Dictionary<string, IEnumerable<Artist>> artists = Client.Browsing.GetArtists(xDoc);
            Assert.AreEqual("A-Ha", artists["A"].First().Name);
            Assert.AreEqual(2, artists["B"].Count());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetArtistTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/artist_example_1.xml");
            Artist artist = Client.Browsing.GetArtist(xDoc);
            Assert.AreEqual(5432, artist.Id);
            Assert.AreEqual("AC/DC", artist.Name);
            Assert.AreEqual(15, artist.AlbumCount);
            Assert.AreEqual(15, artist.Albums.Count());
            Assert.AreEqual(11047, artist.Albums.First().Id);
            Assert.AreEqual(11061, artist.Albums.Last().Id);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetAllAlbumsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            List<Album> albums = Client.Browsing.GetAllAlbums().ToList();
            Assert.IsTrue(albums.Any());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetAlbumTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/album_example_1.xml");
            Album album = Client.Browsing.GetAlbum(xDoc);
            Assert.AreEqual(8, album.SongCount);
            Assert.AreEqual(8, album.Songs.Count());
            Assert.AreEqual("High Voltage", album.Name);
            Assert.AreEqual(71463, album.Songs.First().Id);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetSongTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/song_example_1.xml");
            Child song = Client.Browsing.GetSong(xDoc);
            Assert.AreEqual(48228, song.Id);
            Assert.AreEqual("ACDC/Back in black/ACDC - You Shook Me All Night Long.mp3",song.Path);
            Assert.AreEqual(48203, song.Parent);
            Assert.IsFalse(song.IsDirectory);
            Assert.IsFalse(song.IsVideo == null || song.IsVideo.Value);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetVideosTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/videos_example_1.xml");
            IEnumerable<Child> videos = Client.Browsing.GetVideos(xDoc);
            Assert.AreEqual(7228, videos.First().Id);
            Assert.IsFalse(videos.Any(x => x.IsVideo == false));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetArtistInfoTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/artistInfo_example_1.xml");
            ArtistInfo info = Client.Browsing.GetArtistInfo(xDoc);
            Assert.AreEqual("5182c1d9-c7d2-4dad-afa0-ccfeada921a8", info.MusicBrainzId);
            Assert.AreEqual("http://www.last.fm/music/Black+Sabbath", info.LastFmUrl);
            Assert.AreEqual(3, info.SimilarArtists.Count());
            Assert.AreEqual("Accept", info.SimilarArtists.First().Name);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetArtistInfo2Test()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/artistInfo2_example_1.xml");
            ArtistInfo info = Client.Browsing.GetArtistInfo(xDoc);
            Assert.AreEqual("5182c1d9-c7d2-4dad-afa0-ccfeada921a8", info.MusicBrainzId);
            Assert.AreEqual("http://www.last.fm/music/Black+Sabbath", info.LastFmUrl);
            Assert.AreEqual(8, info.SimilarArtists.Count());
            Assert.AreEqual("Guns N' Roses", info.SimilarArtists.Last().Name);

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetSimilarSongsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/similarSongs_example_1.xml");
            IEnumerable<Child> songs = Client.Browsing.GetSimilarSongs(xDoc);
            Assert.AreEqual(1631, songs.First().Id);
            Assert.AreEqual(10, songs.Count());
            Assert.IsFalse(songs.Any(x => x.IsDirectory));
            Assert.IsFalse(songs.Any(x => x.IsVideo == null || x.IsVideo.Value));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
        [TestMethod]
        public void GetSimilarSongs2Test()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/similarSongs2_example_1.xml");
            IEnumerable<Child> songs = Client.Browsing.GetSimilarSongs(xDoc);
            Assert.AreEqual(1009, songs.First().Id);
            Assert.AreEqual(8, songs.Count());
            Assert.IsFalse(songs.Any(x => x.IsDirectory));
            Assert.IsFalse(songs.Any(x => x.IsVideo == null || x.IsVideo.Value));

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }

        [TestMethod]
        public void GetTopSongsTest()
        {
            // Trust all certificates (even self-signed ones)
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            XDocument xDoc = XDocument.Load("TestData/topSongs_example_1.xml");
            IEnumerable<Child> songs = Client.Browsing.GetTopSongs(xDoc);
            Assert.AreEqual(1013, songs.First().Id);
            Assert.AreEqual(3, songs.Count());
            Assert.IsFalse(songs.Any(x => x.IsVideo == null ||  x.IsDirectory || x.IsVideo.Value ));
            Assert.AreEqual(1, songs.Select(x => x.Artist).Distinct().Count());

            // Reset callback to null for security reasons
            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}
