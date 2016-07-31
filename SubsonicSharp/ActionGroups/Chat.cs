using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Chat
    {
        public SubsonicClient Client { get; set; }

        public Chat(SubsonicClient client)
        {
            Client = client;
        }
        /// <summary>
        /// Returns the current visible (non-expired) chat messages.
        /// </summary>
        /// <param name="since">Only return messages newer than this time</param>
        /// <returns>A collection of ChatMessage objects</returns>
        public IEnumerable<ChatMessage> GetChatMessages(DateTime? since = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getChatMessages";
            if(since.HasValue) command.AddParameter("since", since.Value.ToSecondsFrom1970().ToString());
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(ChatMessage.Create);
        }
        /// <summary>
        /// Adds a message to the chat log.
        /// </summary>
        /// <param name="message">The chat message.</param>
        /// <returns>A bool indicating success ror failure</returns>
        public bool AddChatMessage(string message)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "addChatMessage";
            command.AddParameter("message", message);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}
