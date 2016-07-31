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
        /// <summary>
        /// Returns information about shared media this user is allowed to manage. Takes no extra parameters. 
        /// </summary>
        /// <returns>A collection of Share objects for shares manageable by the current user</returns>
        public IEnumerable<Share> GetShares()
        {
            RestCommand command = new RestCommand {MethodName = "getShares"};
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Share.Create);
        }
        /// <summary>
        /// Creates a public URL that can be used by anyone to stream music or video from the Subsonic server. The URL is short and suitable for posting on Facebook, Twitter etc. Note: The user must be authorized to share (see Settings > Users > User is allowed to share files with anyone). 
        /// </summary>
        /// <param name="ids">ID of a song, album or video to share. Use one id parameter for each entry to share.</param>
        /// <param name="description">A user-defined description that will be displayed to people visiting the shared media.</param>
        /// <param name="expires">The time at which the share expires.</param>
        /// <returns>A Share object for the created share</returns>
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
                    expires.Value.ToSecondsFrom1970().ToString());
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Share.Create).First();
        }
        /// <summary>
        /// Updates the description and/or expiration date for an existing share. 
        /// </summary>
        /// <param name="id">ID of the share to update.</param>
        /// <param name="description"> 	A user-defined description that will be displayed to people visiting the shared media.</param>
        /// <param name="expires">The time at which the share expires.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool UpdateShare(int id, string description = null, DateTime? expires = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "updateShare";
            command.AddParameter("id",id);
            if(!string.IsNullOrEmpty(description)) command.AddParameter("description", description);
            if (expires.HasValue)
                command.AddParameter("expires",
                    expires.Value.ToSecondsFrom1970().ToString());
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Deletes an existing share. 
        /// </summary>
        /// <param name="id">ID of the share to delete.</param>
        /// <returns>A bool indicating success or failure</returns>
        public bool DeleteShare(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deleteShare";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}