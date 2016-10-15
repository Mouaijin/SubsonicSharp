using System;

namespace SubsonicSharp
{
    public class ServerInfo
    {
        public string Host { get; set; } //string to allow for hostname access without IP
        public int Port { get; set; } //Default port for HTTP is 4040
        public string Basepath { get; set; } //Basepath, for example /ampache/
        public int ApiVersion { get; set; } = 11; //Support for later versions to be addressed later
        public Protocol ConnectionProtocol { get; set; } // HTTP/HTTPS protocol, default is HTTP

        public enum Protocol
        {
            Http,
            Https
        }

        public ServerInfo(string host, int port = 4040, string basepath = "", Protocol protocol = Protocol.Http)
        {
            Host = host;
            Port = port;
            Basepath = basepath;
            ConnectionProtocol = protocol;
        }

        public string BaseUrl() => $"{ConnectionProtocol.ToFriendlyString()}{Host}:{Port}{Basepath}";

        public string VersionString() => $"v=1.{ApiVersion}";
    }

    public static class ServerInfoExtensions
    {
        public static string ToFriendlyString(this ServerInfo.Protocol me)
        {
            switch (me)
            {
                case ServerInfo.Protocol.Http:
                    return "http://";
                case ServerInfo.Protocol.Https:
                    return "https://";
                default:
                    throw new ArgumentOutOfRangeException(nameof(me), me, null);
            }
        }
    }
}
