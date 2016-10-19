using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        public SubsonicClient(string username, string password, string address, int port = 4040, int apiVersion = 13,
            string basepath = "", ServerInfo.Protocol protocol = ServerInfo.Protocol.Http)
            : this(new UserToken(username, password), new ServerInfo(address, port, apiVersion, basepath, protocol))
        {
        }

        public SubsonicClient(UserToken user, ServerInfo server)
        {
            if (user.Plaintext == false && server.ApiVersion < 13)
            {
                throw new ApiLevelMismatchException($"Api Level required for salted tokenization: 13+. Server Api Level: {server.ApiVersion}");
            }
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

        private string GetResponse(RestCommand command, [CallerMemberName] string caller = "")
        {
            CheckApiLevelRequirement(caller);
            string url = FormatCommand(command);
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }

        private Task<string> GetResponseAsync(RestCommand command, [CallerMemberName] string caller = "")
        {
            CheckApiLevelRequirement(caller);
            string url = FormatCommand(command);
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url);
            }
        }

        public XDocument GetResponseXDocument(RestCommand command, [CallerMemberName] string caller = "")
        {
            CheckApiLevelRequirement(caller);
            string text = GetResponse(command);
            return XDocument.Parse(text);
        }

        public Task<XDocument> GetResponseXDocumentAsync(RestCommand command, [CallerMemberName] string caller = "")
        {
            CheckApiLevelRequirement(caller);
            return GetResponseAsync(command).ContinueWith(task => XDocument.Parse(task.Result));
        }

        private void CheckApiLevelRequirement(string methodName)
        {
            if (ApiRequirementsCache.MethodApiRequirements.ContainsKey(methodName))
            {
                int reqLevel = ApiRequirementsCache.MethodApiRequirements[methodName];
                if (reqLevel > Server.ApiVersion)
                    throw new ApiLevelMismatchException(
                        $"{methodName}, Api Level Required: {reqLevel}+. Server Api Level: {Server.ApiVersion}");
            }
        }

        #region System

        //Return true on successful ping
        [ApiLevel(0)]
        public bool PingServer()
        {
            RestCommand ping = new RestCommand {MethodName = "ping"};
            XDocument document = GetResponseXDocument(ping);
            return document.Root.Attribute("status").Value.Equals("ok");
        }

        [ApiLevel(0)]
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

        [ApiLevel(2)]
        public JukeboxPlaylist GetJukeboxPlaylist()
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "get");
            return JukeboxPlaylist.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxStats()
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "status");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxSkip(int id = -1, long offset = -1)
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "skip");
            if (id != -1) command.AddParameter("id", id);
            if (offset != -1) command.AddParameter("offset", offset.ToString());
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxRemove(int id)
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "remove");
            command.AddParameter("id", id);
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        public JukeboxStatus JukeboxClear()
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "clear");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxSet(int id)
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "set");
            command.AddParameter("id", id);
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxStart()
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "start");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxStop()
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "stop");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxAdd(int id)
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "add");
            command.AddParameter("id", id);
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxShuffle()
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "shuffle");
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        [ApiLevel(2)]
        public JukeboxStatus JukeboxSetGain(float gain)
        {
            RestCommand command = new RestCommand {MethodName = "jukeboxControl"};
            command.AddParameter("action", "setGain");
            command.AddParameter("gain", gain.ToString());
            return JukeboxStatus.Create(GetResponseXDocument(command).RealRoot());
        }

        #endregion Jukebox
    }

    internal class ApiLevelMismatchException : Exception
    {
        public ApiLevelMismatchException(string message) : base(message)
        {
        }
    }
}