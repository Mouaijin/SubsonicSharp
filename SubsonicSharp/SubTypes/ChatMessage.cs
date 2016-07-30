using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class ChatMessage
    {
        public string Username { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }

        public static ChatMessage Create(XElement xml)
        {
            ChatMessage chat = new ChatMessage();
            chat.Message = xml.AttributeValueOrNull("message");
            chat.Username = xml.AttributeValueOrNull("username");
            if (xml.HasAttribute("time"))
            {
                chat.Time = Convert.ToInt64(xml.Attribute("time").Value).ToDateTimeFrom1970Long();
            }
            return chat;
        }
    }
}
