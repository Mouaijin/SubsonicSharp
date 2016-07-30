using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class Podcasts
    {
        public SubsonicClient Client { get; set; }

        public Podcasts(SubsonicClient client)
        {
            Client = client;
        }

        public IEnumerable<PodcastChannel> GetPodcasts(bool includeEpisodes = true)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPodcasts";
            if (!includeEpisodes) command.AddParameter("includeEpisodes", false);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(PodcastChannel.Create);
        }

        public PodcastChannel GetPodcast(int id, bool includeEpisodes = true)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPodcasts";
            command.AddParameter("id", id);
            if (!includeEpisodes) command.AddParameter("includeEpisodes", false);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(PodcastChannel.Create).First();
        }

        public IEnumerable<PodcastChannel> GetNewestPodcasts(int count = 20)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getNewestPodcasts";
            if (count != 20) command.AddParameter("count", count);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(PodcastChannel.Create);
        }

        public bool RefreshPodcasts()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "refreshPodcasts";
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool CreatePodcastChannel(string url)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createPodcastChannel";
            command.AddParameter("url", url);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool DeletePodcastChannel(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deletePodcastChannel";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool DeletePodcastEpisode(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deletePodcastEpisode";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

        public bool DownloadPodcastEpisode(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "downloadPodcastEpisode";
            command.AddParameter("id",id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

    }
}
