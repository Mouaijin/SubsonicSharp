using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Sharing
    {
        public SubsonicClient Client { get; set; }

        public Sharing(SubsonicClient client)
        {
            Client = client;
        }

        public IEnumerable<Share> GetShares()
        {
            RestCommand command = new RestCommand {MethodName = "getShares"};
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Share.Create);
        }

        public Share CreateShare(IEnumerable<int> ids, string description = null, DateTime? expires = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createShare";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            if (!string.IsNullOrEmpty(description)) command.AddParameter("description", description);
            if (expires.HasValue)
                command.AddParameter("expires",
                    expires.Value.ToUniversalTime()
                        .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds.ToString());
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Share.Create).First();
        }

        public bool UpdateShare(int id, string description = null, DateTime? expires = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "updateShare";
            command.AddParameter("id",id);
            if(!string.IsNullOrEmpty(description)) command.AddParameter("description", description);
            if (expires.HasValue)
                command.AddParameter("expires",
                    expires.Value.ToUniversalTime()
                        .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds.ToString());
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool DeleteShare(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deleteShare";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}