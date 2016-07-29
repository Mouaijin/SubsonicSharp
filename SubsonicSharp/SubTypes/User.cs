using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool ScrobblingEnabled { get; set; }
        public bool AdminRole { get; set; }
        public bool SettingsRole { get; set; }
        public bool DownloadRole { get; set; }
        public bool PlaylistRole { get; set; }
        public bool CoverArtRole { get; set; }
        public bool CommentRole { get; set; }
        public bool PodcastRole { get; set; }
        public bool StreamRole { get; set; }
        public bool JukeboxRole { get; set; }

        public IEnumerable<int> Folders { get; set; }

        public static User Create(XElement xml)
        {
            User user = new User();
            user.Username = xml.Attribute("username").Value;
            if(xml.HasAttribute("email")) user.Email = xml.Attribute("email").Value;
            user.AdminRole = bool.Parse(xml.Attribute("adminRole").Value);
            user.DownloadRole = bool.Parse(xml.Attribute("downloadRole").Value);
            user.PlaylistRole = bool.Parse(xml.Attribute("playlistRole").Value);
            user.SettingsRole = bool.Parse(xml.Attribute("settingsRole").Value);
            user.CoverArtRole = bool.Parse(xml.Attribute("coverArtRole").Value);
            user.CommentRole = bool.Parse(xml.Attribute("commentRole").Value);
            user.PodcastRole = bool.Parse(xml.Attribute("podcastRole").Value);
            user.StreamRole = bool.Parse(xml.Attribute("streamRole").Value);
            user.JukeboxRole = bool.Parse(xml.Attribute("jukeboxRole").Value);
            user.Folders = xml.Elements().Where(x => x.Name.LocalName == "folder").Select(y => Convert.ToInt32(y.Value));
            return user;
        }
    }
}
