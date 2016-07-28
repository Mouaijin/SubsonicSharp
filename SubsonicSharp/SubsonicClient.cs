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

        public Search Search { get; set; }

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
            string expires = licenseElement.LastAttribute.Value;
            //Accessed without name because it varies between "licenseExpires" and "trialExpires"
            return new License(valid, email, expires);
        }

        #endregion System
    }
}