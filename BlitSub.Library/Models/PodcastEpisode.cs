using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    public class PodcastEpisode : MusicDirectory.Entry
    {
        public string EpisodeId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }

        public PodcastEpisode()
        {
            IsDirectory = false;
        }
    }
}
