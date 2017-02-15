using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace blitSub.Domain
{
    public class Artist
    {
        private static string TAG = Artist.class.getSimpleName();
        public static string ROOT_ID = "-1";
	    public static string MISSING_ID = "-2";

        private string id;
        private string name;
        private string index;
        private bool starred;
        private int rating;
        private int closeness;

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
            {
                this.rating = null;
            }
            else
            {
                this.rating = rating;
            }
        }

        public int getCloseness()
        {
            return closeness;
        }
        public void setCloseness(int closeness)
        {
            this.closeness = closeness;
        }

        public bool equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            if (o == null || getClass() != o.getClass())
            {
                return false;
            }

            Artist entry = (Artist)o;
            return id.equals(entry.id);
        }

        public int hashCode()
        {
            return id.hashCode();
        }

        public string toString()
        {
            return name;
        }

        public static class ArtistComparator : Comparator<Artist> {

        private string[] ignoredArticles;
        private Collator collator;

        public ArtistComparator(string[] ignoredArticles)
        {
            this.ignoredArticles = ignoredArticles;
            this.collator = Collator.getInstance(Locale.US);
            this.collator.setStrength(Collator.PRIMARY);
        }

        public int compare(Artist lhsArtist, Artist rhsArtist)
        {
            string lhs = lhsArtist.getName().ToLower();
            string rhs = rhsArtist.getName().ToLower();

            for (string article : ignoredArticles)
            {
                int index = lhs.indexOf(article.toLowerCase() + " ");
                if (index == 0)
                {
                    lhs = lhs.substring(article.length() + 1);
                }
                index = rhs.indexOf(article.toLowerCase() + " ");
                if (index == 0)
                {
                    rhs = rhs.substring(article.length() + 1);
                }
            }

            return collator.compare(lhs, rhs);
        }
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
}
