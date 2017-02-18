using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class PodcastChannel
    {
        private string id;
        private string name;
        private string url;
        private string description;
        private string status;
        private string errorMessage;
        private string coverArt;

        public PodcastChannel()
        {

        }

        public string getId()
        {
            return id;
        }
        public void setId(string id)
        {
            this.id = id;
        }

        public string getName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }

        public string getUrl()
        {
            return url;
        }
        public void setUrl(string url)
        {
            this.url = url;
        }

        public string getDescription()
        {
            return description;
        }
        public void setDescription(string description)
        {
            this.description = description;
        }

        public string getStatus()
        {
            return status;
        }
        public void setStatus(string status)
        {
            this.status = status;
        }

        public string getErrorMessage()
        {
            return errorMessage;
        }
        public void setErrorMessage(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public string getCoverArt()
        {
            return coverArt;
        }
        public void setCoverArt(string coverArt)
        {
            this.coverArt = coverArt;
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            PodcastChannel entry = (PodcastChannel)o;
            return id.Equals(entry.id);
        }

        public class PodcastComparator : IComparer<PodcastChannel> {

        private static string[] ignoredArticles;

        public int Compare(PodcastChannel podcast1, PodcastChannel podcast2)
        {
            string lhs = podcast1.getName();
            string rhs = podcast2.getName();
            if (lhs == null && rhs == null)
            {
                return 0;
            }
            else if (lhs == null)
            {
                return 1;
            }
            else if (rhs == null)
            {
                return -1;
            }

            lhs = lhs.ToLower();
            rhs = rhs.ToLower();

            foreach (string article in ignoredArticles)
            {
                int index = lhs.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
                if (index == 0)
                {
                    lhs = lhs.Substring(article.Length + 1);
                }
                index = rhs.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
                if (index == 0)
                {
                    rhs = rhs.Substring(article.Length + 1);
                }
            }

            return lhs.CompareTo(rhs);
        }

        public static List<PodcastChannel> sort(List<PodcastChannel> podcasts, Context context)
        {
            var prefs = Util.getPreferences(context);
            string ignoredArticlesString = prefs.getString(Constants.CACHE_KEY_IGNORE, "The El La Los Las Le Les");
            ignoredArticles = ignoredArticlesString.Split(" ");

            podcasts.Sort(new PodcastComparator());
            return podcasts;
        }

    }
}
}
