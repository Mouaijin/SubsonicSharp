using System.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Search
    {
        public SubsonicClient Client { get; set; }

        public Search(SubsonicClient client)
        {
            Client = client;
        }
        /// <summary>
        /// Returns albums, artists and songs matching the given search criteria. Supports paging through the result. 
        /// </summary>
        /// <param name="query">Search query. (Basic Regex string)</param>
        /// <param name="artistCount">Maximum number of artists to return. Default: 20</param>
        /// <param name="artistOffset">Search result offset for artists. Used for paging.</param>
        /// <param name="albumCount">Maximum number of albums to return. Default: 20</param>
        /// <param name="albumOffset">Search result offset for artists. Used for paging.</param>
        /// <param name="songCount">Maximum number of albums to return. Default: 20</param>
        /// <param name="songOffset">Search result offset for artists. Used for paging.</param>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A SearchResult object containg lists of matching songs, artists, and albums</returns>
        public SearchResult Search2(string query, int artistCount = 20, int artistOffset = 0, int albumCount = 20,
            int albumOffset = 0, int songCount = 20, int songOffset = 0, int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "search2";
            command.Parameters.Add(new RestParameter("query", query));
            if (artistCount != 20) command.Parameters.Add(new RestParameter("artistCount", artistCount));
            if (artistOffset != 0) command.Parameters.Add(new RestParameter("artistOffset", artistOffset));
            if (albumCount != 20) command.Parameters.Add(new RestParameter("albumCount", albumCount));
            if (albumOffset != 0) command.Parameters.Add(new RestParameter("albumOffset", albumOffset));
            if (songCount != 20) command.Parameters.Add(new RestParameter("songCount", songCount));
            if (songOffset != 0) command.Parameters.Add(new RestParameter("songOffset", songOffset));
            if (musicFolderId >= 0) command.Parameters.Add(new RestParameter("musicFolderId", musicFolderId));
            return SearchResult.Create(Client.GetResponseXDocument(command).RealRoot());
        }
        /// <summary>
        /// Similar to search2, but organizes music according to ID3 tags. 
        /// </summary>
        /// <param name="query">Search query. (Basic Regex string)</param>
        /// <param name="artistCount">Maximum number of artists to return. Default: 20</param>
        /// <param name="artistOffset">Search result offset for artists. Used for paging.</param>
        /// <param name="albumCount">Maximum number of albums to return. Default: 20</param>
        /// <param name="albumOffset">Search result offset for artists. Used for paging.</param>
        /// <param name="songCount">Maximum number of albums to return. Default: 20</param>
        /// <param name="songOffset">Search result offset for artists. Used for paging.</param>
        /// <param name="musicFolderId">Only return results from the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>A SearchResult object containg lists of matching songs, artists, and albums</returns>
        public SearchResult Search3(string query, int artistCount = 20, int artistOffset = 0, int albumCount = 20,
            int albumOffset = 0, int songCount = 20, int songOffset = 0, int musicFolderId = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "search3";
            command.Parameters.Add(new RestParameter("query", query));
            if (artistCount != 20) command.Parameters.Add(new RestParameter("artistCount", artistCount));
            if (artistOffset != 0) command.Parameters.Add(new RestParameter("artistOffset", artistOffset));
            if (albumCount != 20) command.Parameters.Add(new RestParameter("albumCount", albumCount));
            if (albumOffset != 0) command.Parameters.Add(new RestParameter("albumOffset", albumOffset));
            if (songCount != 20) command.Parameters.Add(new RestParameter("songCount", songCount));
            if (songOffset != 0) command.Parameters.Add(new RestParameter("songOffset", songOffset));
            if (musicFolderId >= 0) command.Parameters.Add(new RestParameter("musicFolderId", musicFolderId));
            return SearchResult.Create(Client.GetResponseXDocument(command).RealRoot());
        }
    }
}