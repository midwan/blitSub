using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class PodcastChannel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string CoverArt { get; set; }

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

            var entry = (PodcastChannel)o;
            return Id.Equals(entry.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public class PodcastComparator : IComparer<PodcastChannel>
        {

            private static string[] _ignoredArticles;

            public int Compare(PodcastChannel podcast1, PodcastChannel podcast2)
            {
                var lhs = podcast1?.Name;
                var rhs = podcast2?.Name;
                if (lhs == null && rhs == null)
                {
                    return 0;
                }

                if (lhs == null)
                {
                    return 1;
                }

                if (rhs == null)
                {
                    return -1;
                }

                lhs = lhs.ToLower();
                rhs = rhs.ToLower();

                foreach (var article in _ignoredArticles)
                {
                    var index = lhs.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
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

                return string.Compare(lhs, rhs, StringComparison.Ordinal);
            }

            public static List<PodcastChannel> Sort(List<PodcastChannel> podcasts)
            {
                //var prefs = Util.getPreferences(context);
                var ignoredArticlesString = "The El La Los Las Le Les"; //prefs.getString(Constants.CACHE_KEY_IGNORE, "The El La Los Las Le Les");
                _ignoredArticles = ignoredArticlesString.Split(" ");

                podcasts.Sort(new PodcastComparator());
                return podcasts;
            }

        }
    }
}
