using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.ActionGroups
{
    public class MediaAnnotation
    {
        public SubsonicClient Client { get; set; }

        public MediaAnnotation(SubsonicClient client)
        {
            Client = client;
        }

        public bool Star(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "star";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool StarAlbums(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "star";
            foreach (int id in ids)
            {
                command.AddParameter("albumId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool StarArtists(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "star";
            foreach (int id in ids)
            {
                command.AddParameter("artistId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        public bool Unstar(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "unstar";
            foreach (int id in ids)
            {
                command.AddParameter("id", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool UnstarAlbums(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "unstar";
            foreach (int id in ids)
            {
                command.AddParameter("albumId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool UnstarArtists(IEnumerable<int> ids)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "unstar";
            foreach (int id in ids)
            {
                command.AddParameter("artistId", id);
            }
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool SetRating(int id, int rating)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "setRating";
            command.AddParameter("id",id);
            command.AddParameter("rating", rating);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";

        }

        public bool Scrobble(int id, DateTime? time = null, bool submission = true)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "scrobble";
            command.AddParameter("id", id);
            if(time.HasValue) command.AddParameter("time", time.Value.ToSecondsFrom1970().ToString());
            if(submission == false) command.AddParameter("submission", false);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
    }
}
