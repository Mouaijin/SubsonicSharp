using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubsonicSharp;
using SubsonicSharp.SubTypes;

namespace Tests
{
    [TestClass]
    public class BrowsingTests
    {
        public SubsonicClient Client { get; }
        public BrowsingTests()
        {
            UserToken user = new UserToken("test", "test", true);
            ServerInfo server = new ServerInfo("192.168.1.140");
            Client = new SubsonicClient(user, server);
        }
        [TestMethod]
        public void GetMusicFoldersTest()
        {
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
        }

        [TestMethod]
        public void GetIndexesTest()
        {
            XDocument xDoc = XDocument.Load("TestData/indexes_example_1.xml");
            IndexesCollection collection = Client.Browsing.GetIndexes(xDoc);
            Assert.AreEqual(2, collection.Shortcuts.Count(), "Bad shorcuts count");
            Assert.AreEqual(2, collection.Indexes.Keys.Count, "Bad dictionary key count");
            Assert.AreEqual("Bob Dylan", collection.Indexes["B"].Single().Name, "Bob Dylan as only artist in 'B' failed");
            Assert.AreEqual(2, collection.Children.Count());
            Assert.IsTrue(collection.Children.Count(x => x.Id == 111) == 1);
        }

        [TestMethod]
        public void GetMusicDirectoryTest()
        {
            XDocument xDoc = XDocument.Load("TestData/directory_example_2.xml");
            Directory dir = Client.Browsing.GetMusicDirectory(xDoc);
            Assert.AreEqual(2, dir.Children.Count());
            Assert.AreEqual(11, dir.Id);
            Assert.AreEqual("Arrival", dir.Name);
            Assert.AreEqual(1, dir.Parent);
        }

        [TestMethod]
        public void GetGenresTest()
        {
            XDocument xDoc = XDocument.Load("TestData/genres_example_1.xml");
            IEnumerable<Genre> genres = Client.Browsing.GetGenres(xDoc);
            Assert.AreEqual("Electronic", genres.First().Name);
            Assert.AreEqual(28, genres.First().SongCount);
            Assert.AreEqual(7, genres.Count());
        }

        [TestMethod]
        public void GetArtistsTest()
        {
            XDocument xDoc =XDocument.Load("TestData/artists_example_1.xml");
            Dictionary<string, IEnumerable<Artist>> artists = Client.Browsing.GetArtists(xDoc);
            Assert.AreEqual("A-Ha", artists["A"].First().Name);
            Assert.AreEqual(2, artists["B"].Count());
        }

        [TestMethod]
        public void GetArtistTest()
        {
            XDocument xDoc = XDocument.Load("TestData/artist_example_1.xml");
            Artist artist = Client.Browsing.GetArtist(xDoc);
            Assert.AreEqual(5432, artist.Id);
            Assert.AreEqual("AC/DC", artist.Name);
            Assert.AreEqual(15, artist.AlbumCount);
            Assert.AreEqual(15, artist.Albums.Count());
            Assert.AreEqual(11047, artist.Albums.First().Id);
            Assert.AreEqual(11061, artist.Albums.Last().Id);
        }

        [TestMethod]
        public void GetAlbumTest()
        {
            XDocument xDoc = XDocument.Load("TestData/album_example_1.xml");
            Album album = Client.Browsing.GetAlbum(xDoc);
            Assert.AreEqual(8, album.SongCount);
            Assert.AreEqual(8, album.Songs.Count());
            Assert.AreEqual("High Voltage", album.Name);
            Assert.AreEqual(71463, album.Songs.First().Id);
        }

        [TestMethod]
        public void GetSongTest()
        {
            XDocument xDoc = XDocument.Load("TestData/song_example_1.xml");
            Child song = Client.Browsing.GetSong(xDoc);
            Assert.AreEqual(48228, song.Id);
            Assert.AreEqual("ACDC/Back in black/ACDC - You Shook Me All Night Long.mp3",song.Path);
            Assert.AreEqual(48203, song.Parent);
            Assert.IsFalse(song.IsDirectory);
            Assert.IsFalse(song.IsVideo.Value);
        }

        [TestMethod]
        public void GetVideosTest()
        {
            XDocument xDoc = XDocument.Load("TestData/videos_example_1.xml");
            IEnumerable<Child> videos = Client.Browsing.GetVideos(xDoc);
            Assert.AreEqual(7228, videos.First().Id);
            Assert.IsFalse(videos.Any(x => x.IsVideo == false));
        }

        [TestMethod]
        public void GetArtistInfoTest()
        {
            XDocument xDoc = XDocument.Load("TestData/artistInfo_example_1.xml");
            ArtistInfo info = Client.Browsing.GetArtistInfo(xDoc);
            Assert.AreEqual("5182c1d9-c7d2-4dad-afa0-ccfeada921a8", info.MusicBrainzId);
            Assert.AreEqual("http://www.last.fm/music/Black+Sabbath", info.LastFmUrl);
            Assert.AreEqual(3, info.SimilarArtists.Count());
            Assert.AreEqual("Accept", info.SimilarArtists.First().Name);
        }

        [TestMethod]
        public void GetArtistInfo2Test()
        {
            XDocument xDoc = XDocument.Load("TestData/artistInfo2_example_1.xml");
            ArtistInfo info = Client.Browsing.GetArtistInfo(xDoc);
            Assert.AreEqual("5182c1d9-c7d2-4dad-afa0-ccfeada921a8", info.MusicBrainzId);
            Assert.AreEqual("http://www.last.fm/music/Black+Sabbath", info.LastFmUrl);
            Assert.AreEqual(8, info.SimilarArtists.Count());
            Assert.AreEqual("Guns N' Roses", info.SimilarArtists.Last().Name);
        }

        [TestMethod]
        public void GetSimilarSongsTest()
        {
            XDocument xDoc = XDocument.Load("TestData/similarSongs_example_1.xml");
            IEnumerable<Child> songs = Client.Browsing.GetSimilarSongs(xDoc);
            Assert.AreEqual(1631, songs.First().Id);
            Assert.AreEqual(10, songs.Count());
            Assert.IsFalse(songs.Any(x => x.IsDirectory));
            Assert.IsFalse(songs.Any(x => x.IsVideo.Value == true));
        }
        [TestMethod]
        public void GetSimilarSongs2Test()
        {
            XDocument xDoc = XDocument.Load("TestData/similarSongs2_example_1.xml");
            IEnumerable<Child> songs = Client.Browsing.GetSimilarSongs(xDoc);
            Assert.AreEqual(1009, songs.First().Id);
            Assert.AreEqual(8, songs.Count());
            Assert.IsFalse(songs.Any(x => x.IsDirectory));
            Assert.IsFalse(songs.Any(x => x.IsVideo.Value == true));
        }
    }
}
