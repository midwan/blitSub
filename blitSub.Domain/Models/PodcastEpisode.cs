using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class PodcastEpisode : MusicDirectory.Entry
    {
        private string episodeId;
        private string date;
        private string status;

        public PodcastEpisode()
        {
            SetDirectory(false);
        }

        public string getEpisodeId()
        {
            return episodeId;
        }
        public void setEpisodeId(string episodeId)
        {
            this.episodeId = episodeId;
        }

        public string getDate()
        {
            return date;
        }
        public void setDate(string date)
        {
            this.date = date;
        }

        public string getStatus()
        {
            return status;
        }
        public void setStatus(string status)
        {
            this.status = status;
        }
    }
}
