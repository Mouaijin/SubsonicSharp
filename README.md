# SubsonicSharp
PCL for accessing the Subsonic Music Server API in .NET without actually using the REST API

## Goals
1. Allow for fluent OOP use of the Subsonic REST API in .Net
2. Create a completely portable library for development of Subsonic clients on any device
3. Lower the barrier to entry drastically for use of the API

##Installation
You can build and include this binary manually, or retrieve the package using nuget: 
[SubsonicSharp.PCL](https://www.nuget.org/packages/SubsonicSharp.PCL)

##Use
Create a `SubsonicClient` object from the main package by passing in user and server information strings, and then call the methods needed as listed [in the official API documentation](http://www.subsonic.org/pages/api.jsp). These methods are sometimes grouped into property classes according to function groups defined in the API specification, such as `MediaRetrieval` or `ClientBrowser`. More information on use and syntax can be found in the test files for the project.

####Media Retrieval Note
This library currently supports passing the URL string needed to stream or download files, but does not pass the data itself.
This may be addressed with platform-specific DI projects in the future (pull requests welcome), but due to the differences in how each platform handles media retrieval, this is a secondary goal.

##SSL Support
Because it is not possible to authenticate SSL certificates from a PCL at this time, to use this library with HTTPS, it is necessary to add some code to the consuming project. The easiest way to trust all certificates is to add the line:
`ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;`


This will modify the validation method for the entire application domain, so it is suggested that one of two actions is taken:

1. Reset this callback to `null` after making a call to the API
2. Set the callback to use a custom authentication method, rather than just returning true

####Contributions to address this problem with a standard DI approach for specific platforms welcomed



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
  - Get Album Info (1.14+)
  - Get Album Info 2 (1.14+)
  - Get Artist Info
  - Get Artist Info 2
  - Get Similar Songs
  - Get Similar Songs 2
  - Get Top Songs
  - Get Video Info (1.14+)
- Media Retrieval
  - Stream
  - Download
  - HLS
  - Get Cover Art
  - Get Lyrics
  - Get Captions
  - Get Avatar
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
- Chat
  - Get Chat Messages
  - Add Chat Message
- Jukebox
  - Jukebox Control

###Incomplete

- Searching
  - Search (deprecated in 1.4, will not implement)
