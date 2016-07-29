using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Bookmarks
    {
        public SubsonicClient Client { get; set; }

        public Bookmarks(SubsonicClient client)
        {
            Client = client;
        }

        public IEnumerable<Bookmark> GetBookmarks()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getBookmarks";
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(Bookmark.Create);
        }

        public bool CreateBookmark(int id, long position, string comment = null)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createBookmark";
            command.AddParameter("id", id);
            command.AddParameter("position", position.ToString());
            if(!string.IsNullOrEmpty(comment)) command.AddParameter("comment", comment);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool DeleteBookmark(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deleteBookmark";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";

        }

        public PlayQueue GetPlayQueue()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPlayQueue";
            return PlayQueue.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        public bool SavePlayQueue(IEnumerable<int> ids, int currentId = -1, long position = -1)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "savePlayQueue";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            if(currentId != -1) command.AddParameter("current", currentId);
            if(position != -1) command.AddParameter("position", position.ToString());
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";

        }
    }
}
