namespace SubsonicSharp
{
    public class ServerInfo
    {
        public string Host { get; set; } //string to allow for hostname access without IP
        public int Port { get; set; } //Default port for HTTP is 4040
        public string Basepath { get; set; } //Basepath, for example /ampache/
        public int ApiVersion { get; set; } = 13; //Support for later versions to be addressed later
        public Protocol ConnectionProtocol { get; set; } // HTTP/HTTPS protocol, default is HTTP

        public enum Protocol
        {
            Http,
            Https
        }

        public ServerInfo(string host, int port = 4040, int apiVersion = 13, string basepath = "", Protocol protocol = Protocol.Http)
        {
            Host = host;
            Port = port;
            Basepath = basepath;
            ConnectionProtocol = protocol;
            ApiVersion = apiVersion;
        }

        public string BaseUrl() => $"{ConnectionProtocol.ToFriendlyString()}{Host}:{Port}{Basepath}";

        public string VersionString() => $"v=1.{ApiVersion}";
    }
}
