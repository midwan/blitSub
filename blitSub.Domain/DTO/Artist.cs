using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Artist
    {
        private static readonly string TAG = Artist.getSimpleName();
        public static string ROOT_ID = "-1";
        public static string MISSING_ID = "-2";
        private int closeness;

        private string id;
        private string index;
        private string name;
        private int rating;
        private bool starred;

        public Artist()
        {
        }

        public Artist(string id, string name)
        {
            this.id = id;
            this.name = name;
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

        public string getIndex()
        {
            return index;
        }

        public void setIndex(string index)
        {
            this.index = index;
        }

        public bool isStarred()
        {
            return starred;
        }

        public void setStarred(bool starred)
        {
            this.starred = starred;
        }

        public int getRating()
        {
            return rating == null ? 0 : rating;
        }

        public void setRating(int rating)
        {
            if (rating == null || rating == 0)
                this.rating = null;
            else
                this.rating = rating;
        }

        public int getCloseness()
        {
            return closeness;
        }

        public void setCloseness(int closeness)
        {
            this.closeness = closeness;
        }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (o == null || getClass() != o.getClass())
                return false;

            var entry = (Artist) o;
            return id.equals(entry.id);
        }

        public int hashCode()
        {
            return id.hashCode();
        }

        public override string ToString()
        {
            return name;
        }

        public static void sort(List<Artist> artists, string[] ignoredArticles)
        {
            try
            {
                Collections.sort(artists, new ArtistComparator(ignoredArticles));
            }
            catch (Exception e)
            {
                Log.w(TAG, "Failed to sort artists", e);
            }
        }

        public static class ArtistComparator : Comparator<Artist>
        {
            private readonly Collator collator;

            private readonly string[] ignoredArticles;

            public ArtistComparator(string[] ignoredArticles)
            {
                this.ignoredArticles = ignoredArticles;
                collator = Collator.getInstance(Locale.US);
                collator.setStrength(Collator.PRIMARY);
            }

            public int compare(Artist lhsArtist, Artist rhsArtist)
            {
                string lhs = lhsArtist.getName().toLowerCase();
                string rhs = rhsArtist.getName().toLowerCase();

                foreach (var article in ignoredArticles)
                {
                    int index = lhs.indexOf(article.toLowerCase() + " ");
                    if (index == 0)
                        lhs = lhs.substring(article.length() + 1);
                    index = rhs.indexOf(article.toLowerCase() + " ");
                    if (index == 0)
                        rhs = rhs.substring(article.length() + 1);
                }

                return collator.compare(lhs, rhs);
            }
        }
    }
}