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
        /// <summary>
        /// Returns all Podcast channels the server subscribes to, and (optionally) their episodes. A typical use case for this method would be to first retrieve all channels without episodes, and then retrieve all episodes for the single channel the user selects. 
        /// </summary>
        /// <param name="includeEpisodes">Whether to include Podcast episodes in the returned result.</param>
        /// <returns>A collection of all PodcastChannel objects in system</returns>
        [ApiLevel(6)]
        public IEnumerable<PodcastChannel> GetPodcasts(bool includeEpisodes = true)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPodcasts";
            if (!includeEpisodes) command.AddParameter("includeEpisodes", false);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(PodcastChannel.Create);
        }
        /// <summary>
        /// Returns the podcast with the specified id. A typical use case for this method would be to first retrieve all channels without episodes, and then retrieve all episodes for the single channel the user selects. 
        /// </summary>
        /// <param name="id">Only return the Podcast channel with this ID.</param>
        /// <param name="includeEpisodes">Whether to include Podcast episodes in the returned result.</param>
        /// <returns>The requested PodcastChannel object</returns>
        [ApiLevel(6)]
        public PodcastChannel GetPodcast(int id, bool includeEpisodes = true)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getPodcasts";
            command.AddParameter("id", id);
            if (!includeEpisodes) command.AddParameter("includeEpisodes", false);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(PodcastChannel.Create).First();
        }
        /// <summary>
        /// Returns the most recently published Podcast episodes. 
        /// </summary>
        /// <param name="count">The maximum number of episodes to return.</param>
        /// <returns>A collection of the newest PodcastChannel objects</returns>
        [ApiLevel(13)]
        public IEnumerable<PodcastChannel> GetNewestPodcasts(int count = 20)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getNewestPodcasts";
            if (count != 20) command.AddParameter("count", count);
            return Client.GetResponseXDocument(command).RealRoot().Elements().Select(PodcastChannel.Create);
        }
        /// <summary>
        /// Requests the server to check for new Podcast episodes. Note: The user must be authorized for Podcast administration (see Settings &gt; Users &gt; User is allowed to administrate Podcasts). 
        /// </summary>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(9)]
        public bool RefreshPodcasts()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "refreshPodcasts";
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Adds a new Podcast channel. Note: The user must be authorized for Podcast administration (see Settings &gt; Users &gt; User is allowed to administrate Podcasts). 
        /// </summary>
        /// <param name="url">The URL of the Podcast to add.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(9)]
        public bool CreatePodcastChannel(string url)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "createPodcastChannel";
            command.AddParameter("url", url);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Deletes a Podcast channel. Note: The user must be authorized for Podcast administration (see Settings &gt; Users &gt; User is allowed to administrate Podcasts). 
        /// </summary>
        /// <param name="id">The ID of the Podcast channel to delete.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(9)]
        public bool DeletePodcastChannel(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deletePodcastChannel";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Deletes a Podcast episode. Note: The user must be authorized for Podcast administration (see Settings &gt; Users &gt; User is allowed to administrate Podcasts). 
        /// </summary>
        /// <param name="id">The ID of the Podcast episode to delete.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(9)]
        public bool DeletePodcastEpisode(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "deletePodcastEpisode";
            command.AddParameter("id", id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }
        /// <summary>
        /// Request the server to start downloading a given Podcast episode. Note: The user must be authorized for Podcast administration (see Settings &gt; Users &gt; User is allowed to administrate Podcasts). 
        /// </summary>
        /// <param name="id">The ID of the Podcast episode to download.</param>
        /// <returns>A bool indicating success or failure</returns>
        [ApiLevel(9)]
        public bool DownloadPodcastEpisode(int id)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "downloadPodcastEpisode";
            command.AddParameter("id",id);
            return Client.GetResponseXDocument(command).Root.Attribute("status").Value == "ok";
        }

    }
}
