using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class InformationLists
    {
        public SubsonicClient Client { get; set; }

        public InformationLists(SubsonicClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Returns a list of random, newest, highest rated etc. albums. Similar to the album lists on the home page of the Subsonic web interface. 
        /// </summary>
        /// <param name="type">The list type. Must be one of the following: random, newest, highest, frequent, recent. Since 1.8.0 you can also use alphabeticalByName or alphabeticalByArtist to page through all albums alphabetically, and starred to retrieve starred albums. Since 1.10.1 you can use byYear and byGenre to list albums in a given year range or genre. </param>
        /// <param name="size">The number of albums to return. Max 500.</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums.</param>
        /// <param name="fromYear">The first year in the range. If fromYear > toYear a reverse chronological list is returned.</param>
        /// <param name="toYear">The last year in the range.</param>
        /// <param name="genre">The name of the genre, e.g., "Rock".</param>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A list of albums matching specified options</returns>
        public IEnumerable<Album> GetAlbumList(ListOrdering type, int size = 10, int offset = 0, int fromYear = -1,
            int toYear = -1, string genre = null, int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getAlbumList";
            command.AddParameter("type", type.ConvertToString());
            if (size != 10) command.AddParameter("size", size);
            if (offset != 0) command.AddParameter("offset", offset);
            if (type == ListOrdering.ByYear)
            {
                command.AddParameter("fromYear", fromYear);
                command.AddParameter("toYear", toYear);
            }
            if (!string.IsNullOrEmpty(genre)) command.AddParameter("genre", genre);
            if (musicFolderId >= 0) command.AddParameter("musicFolderId", musicFolderId);
            foreach (XElement element in Client.GetResponseXDocument(command).RealRoot().Elements())
            {
                yield return Album.Create(element);
            }
        }

        /// <summary>
        /// Similar to GetAlbumList, but organizes music according to ID3 tags. 
        /// </summary>
        /// <param name="type">The list type. Must be one of the following: random, newest, highest, frequent, recent. Since 1.8.0 you can also use alphabeticalByName or alphabeticalByArtist to page through all albums alphabetically, and starred to retrieve starred albums. Since 1.10.1 you can use byYear and byGenre to list albums in a given year range or genre. </param>
        /// <param name="size">The number of albums to return. Max 500.</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums.</param>
        /// <param name="fromYear">The first year in the range. If fromYear > toYear a reverse chronological list is returned.</param>
        /// <param name="toYear">The last year in the range.</param>
        /// <param name="genre">The name of the genre, e.g., "Rock".</param>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A list of albums matching specified options</returns>
        public IEnumerable<Album> GetAlbumList2(ListOrdering type, int size = 10, int offset = 0, int fromYear = -1,
            int toYear = -1, string genre = null, int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getAlbumList2";
            command.AddParameter("type", type.ConvertToString());
            if (size != 10) command.AddParameter("size", size);
            if (offset != 0) command.AddParameter("offset", offset);
            if (type == ListOrdering.ByYear)
            {
                command.AddParameter("fromYear", fromYear);
                command.AddParameter("toYear", toYear);
            }
            if (!string.IsNullOrEmpty(genre)) command.AddParameter("genre", genre);
            if (musicFolderId >= 0) command.AddParameter("musicFolderId", musicFolderId);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Album.Create);
        }

        /// <summary>
        /// Returns random songs matching the given criteria. 
        /// </summary>
        /// <param name="size">The maximum number of songs to return. Max 500. Default: 10</param>
        /// <param name="genre">Only returns songs belonging to this genre.</param>
        /// <param name="fromYear">Only return songs published after or in this year.</param>
        /// <param name="toYear">Only return songs published before or in this year.</param>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A random list of songs matching specified options</returns>
        public IEnumerable<Child> GetRandomSongs(int size = 10, string genre = null, int fromYear = -1, int toYear = -1,
            int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getRandomSongs";
            if (size != 10) command.AddParameter("size", size);
            if (!string.IsNullOrEmpty(genre)) command.AddParameter("genre", genre);
            if (fromYear != -1) command.AddParameter("fromYear", fromYear);
            if (toYear != -1) command.AddParameter("toYear", toYear);
            if (musicFolderId >= 0) command.AddParameter("musicfolderId", musicFolderId);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Child.Create);
        }

        /// <summary>
        /// Returns songs in a given genre. 
        /// </summary>
        /// <param name="genre">The genre, as returned by GetGenres.</param>
        /// <param name="count">The maximum number of songs to return. Max 500. Default 10.</param>
        /// <param name="offset">The offset. Useful if you want to page through the songs in a genre.</param>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A list of songs belonging to the specified genre</returns>
        public IEnumerable<Child> GetSongsByGenre(string genre, int count = 10, int offset = 0, int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getSongsByGenre";
            command.AddParameter("genre", genre);
            if (count != 10) command.AddParameter("count", count);
            if (offset != 0) command.AddParameter("offset", offset);
            if (musicFolderId >= 0) command.AddParameter("musicFolderId", musicFolderId);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Child.Create);
        }

        /// <summary>
        /// Returns what is currently being played by all users. Takes no extra parameters. 
        /// </summary>
        /// <returns>A list of NowPlaying objects- a song with info about its user and player</returns>
        public IEnumerable<NowPlaying> GetNowPlaying()
        {
            return
                Client.GetResponseXDocument(new RestCommand {MethodName = "getNowPlaying"})
                    .Root.Elements()
                    .First()
                    .Elements()
                    .Select(NowPlaying.Create);
        }

        /// <summary>
        /// Returns starred songs, albums and artists. 
        /// </summary>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A SearchResult containing lists of starred Albums, Artists, and Songs</returns>
        public SearchResult GetStarred(int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getStarred";
            if (musicFolderId >= 0) command.AddParameter("musicFolderId", musicFolderId);
            return SearchResult.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        /// <summary>
        /// Similar to GetStarred, but organizes music according to ID3 tags. 
        /// </summary>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A SearchResult containing lists of starred Albums, Artists, and Songs</returns>
        public SearchResult GetStarred2(int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getStarred2";
            if (musicFolderId >= 0) command.AddParameter("musicFolderId", musicFolderId);
            return SearchResult.Create(Client.GetResponseXDocument(command).RealRoot());
        }
    }


    public enum ListOrdering
    {
        Random,
        Newest,
        Highest,
        Frequent,
        Recent,
        AlphabeticalByName,
        AlphabeticalByArtist,
        Starred,
        ByYear,
        ByGenre
    }
}