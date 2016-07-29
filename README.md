# SubsonicSharp
PCL for accessing the Subsonic Music Server API in .NET without actually using the REST API

## Goals
1. Allow for fluent OOP use of the Subsonic REST API in .Net
2. Create a completely portable library for development of Subsonic clients on any device
3. Lower the barrier to entry drastically for use of the API

####Media Retrieval Note
This library currently supports passing the URL string needed to stream or download files, but does not pass the data itself.
This may be addressed with platform-specific DI projects in the future (pull requests welcome), but due to the differences in how each platform handles media retrieval, this is a secondary goal.

##API Coverage Progress

###Complete

- System
  - Ping
  - Get License
- Browsing
  - GetMusicFolders
  - Get Indexes
  - Get Music Directory
  - Get Genres
  - Get Artists
  - Get Artist
  - Get Album
  - Get Song
  - Get Videos
  - Get Artist Info
  - Get Artist Info 2
  - Get Similar Songs
  - Get Similar Songs 2
  - Get Top Songs
- Media Retrieval
  - Stream
  - Download
  - Get Cover Art
  - Get Lyrics
- Album/Song List
  - Get Album List
  - Get Album List 2
  - Get Random Songs
  - Get Songs By Genre
  - Get Now Playing
  - Get Starred
  - Get Starred 2
- Searching
  - Search 2
  - Search 3
- Playlists
  - GetPlaylists
  - Get Playlist
  - Create Playlist
  - Update Playlist
  - Delete Playlist
- Sharing
  - Get Shares
  - Create Share
  - Update Share
  - Delete Share
- User Management
  - Get User
  - Get Users
  - Create User
  - Update User
  - Delete User
  - Change Password
- Bookmarks
 - Get Bookmarks
 - Create Bookmark
 - Delete Bookmarks
 - Get Play Queue
 - Save Play Queue

###Incomplete

- Browsing
  - Get Video Info (1.14+)
  - Get Album Info (1.14+)
  - Get Album Info 2 (1.14+)
- Searching
  - Search (deprecated in 1.4, will not implement)
- Media Retrieval
  - HLS
  - Get Captions
  - Get Avatar
- Media Annotation
  - Star
  - Unstar
  - Set Rating
  - Scrobble
- Podcast
  - Get Podcasts
  - Get Newest Podcasts
  - Refresh Podcasts
  - Create Podcast Channel
  - Delete Podcast Channel
  - Delete Podcast Episode
  - Download Podcast Episode
- Jukebox
  - Jukebox Control
- Chat
  - Get Chat Messages
  - Add Chat Message
