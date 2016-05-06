using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp
{
    public class ServerInfo
    {
        public string Address { get; set; } //string to account to allow for hostname access without IP
        public int Port { get; set; } //Default port for HTTP is 4040
        public int ApiVersion { get; set; } = 13; //Support for later versions to be addressed later

        public ServerInfo(string address, int port = 4040)
        {
            Address = address;
            Port = port;
        }

        public string BaseUrl() => $"http://{Address}:{Port}";

        public string VersionString() => $"v=1.{ApiVersion}";
    }
}
