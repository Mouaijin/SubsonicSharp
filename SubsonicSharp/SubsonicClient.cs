using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using SubsonicSharp.ActionGroups;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp
{
    public class SubsonicClient
    {
        public UserToken User { get; }
        public ServerInfo Server { get; }
        public string ClientName { get; } = "SubSharp"; //Required field for requests

        public ClientBrowser Browsing { get; set; }
        public MediaRetrieval MediaRetrieval { get; set; }
        public InformationLists InformationLists { get; set; }
        public UserManagement UserManagement { get; set; }
        public Sharing Sharing { get; set; }

        public Search Search { get; set; }
        public Bookmarks Bookmarks { get; set; }
        public MediaAnnotation MediaAnnotation { get; set; }
        public Playlists Playlists { get; set; }
        public Podcasts Podcasts { get; set; }
        public Chat Chat { get; set; }

        public SubsonicClient(string username, string password, string address, int port = 4040)
            : this(new UserToken(username, password), new ServerInfo(address, port))
        {
        }

        public SubsonicClient(UserToken user, ServerInfo server)
        {
            User = user;
            Server = server;
            Browsing = new ClientBrowser(this);
            MediaRetrieval = new MediaRetrieval(this);
            Search = new Search(this);
            InformationLists = new InformationLists(this);
            UserManagement = new UserManagement(this);
            Sharing = new Sharing(this);
            Bookmarks = new Bookmarks(this);
            MediaAnnotation = new MediaAnnotation(this);
            Playlists = new Playlists(this);
            Podcasts = new Podcasts(this);
            Chat = new Chat(this);
        }

        public string FormatCommand(RestCommand command)
        {
            return
                $"{Server.BaseUrl()}/rest/{command.MethodName}?{command.ParameterString()}{User}&{Server.VersionString()}&c={ClientName}";
        }

        private string GetResponse(RestCommand command)
        {
            string url = FormatCommand(command);
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }

        private Task<string> GetResponseAsync(RestCommand command)
        {
            string url = FormatCommand(command);
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url);
            }
        }

        public XDocument GetResponseXDocument(RestCommand command)
        {
            string text = GetResponse(command);
            return XDocument.Parse(text);
        }

        public Task<XDocument> GetResponseXDocumentAsync(RestCommand command)
        {
            return GetResponseAsync(command).ContinueWith(task => XDocument.Parse(task.Result));
        }

        #region System

        //Return true on successful ping
        public bool PingServer()
        {
            RestCommand ping = new RestCommand {MethodName = "ping"};
            XDocument document = GetResponseXDocument(ping);
            return document.Root.Attribute("status").Value.Equals("ok");
        }

        public License GetLicense()
        {
            RestCommand licenseCommand = new RestCommand {MethodName = "getLicense"};
            XDocument document = GetResponseXDocument(licenseCommand);
            XElement licenseElement = document.Root.Descendants().First();
            string valid = licenseElement.Attribute("valid").Value;
            string email = licenseElement.Attribute("email").Value;
            string expires = "";
            if (licenseElement.HasAttribute("licenseExpires"))
                expires = licenseElement.Attribute("licenseExpires").Value;
            else if (licenseElement.HasAttribute("trialExpires"))
                expires = licenseElement.Attribute("trialExpires").Value;
            else expires = "1999-06-14T14:05:06.578Z";
            return new License(valid, email, expires);
        }

        #endregion System

        #region Jukebox

        public JukeboxPlaylist GetJukeboxPlaylist()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "get");
            return JukeboxPlaylist.Create(GetResponseXDocument(command).RealRoot());
        }

        public JukeboxStatus JukeboxStats()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action","status");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        public JukeboxStatus JukeboxSkip(int id = -1, long offset = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "skip");
            if(id != -1) command.AddParameter("id", id);
            if(offset != -1) command.AddParameter("offset", offset.ToString());
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxRemove(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "remove");
            command.AddParameter("id", id);
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxClear()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "clear");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxSet(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "set");
            command.AddParameter("id", id);
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxStart()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "start");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxStop()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "stop");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxAdd(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "add");
            command.AddParameter("id", id);
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxShuffle()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "shuffle");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }
        public JukeboxStatus JukeboxSetGain(float gain)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "jukeboxControl";
            command.AddParameter("action", "setGain");
            command.AddParameter("gain", gain.ToString());
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());

        }




        #endregion Jukebox
    }
}