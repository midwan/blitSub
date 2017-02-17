using System;
using System.Collections.Generic;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class Artist
    {
        public static string ROOT_ID = "-1";
        public static string MISSING_ID = "-2";
        private readonly string TAG;
        private int _closeness;

        private string _id;
        private string _index;
        private string _name;
        private int? _rating;
        private bool _starred;

        public Artist()
        {
            TAG = GetName();
        }

        public Artist(string id, string name)
        {
            _id = id;
            TAG = GetName();
            _name = name;
        }

        public string GetId()
        {
            return _id;
        }

        public void SetId(string id)
        {
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetIndex()
        {
            return _index;
        }

        public void SetIndex(string index)
        {
            _index = index;
        }

        public bool IsStarred()
        {
            return _starred;
        }

        public void SetStarred(bool starred)
        {
            _starred = starred;
        }

        public int GetRating()
        {
            return _rating ?? 0;
        }

        public void SetRating(int? rating)
        {
            if (rating == null || rating == 0)
                _rating = null;
            else
                _rating = rating;
        }

        public int GetCloseness()
        {
            return _closeness;
        }

        public void SetCloseness(int closeness)
        {
            _closeness = closeness;
        }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (o == null || GetType() != o.GetType())
                return false;

            var entry = (Artist) o;
            return _id.Equals(entry._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return _name;
        }

        public static void Sort(List<Artist> artists, string[] ignoredArticles)
        {
            try
            {
                artists.Sort(new ArtistComparator(ignoredArticles));
            }
            catch (Exception e)
            {
                Log.w(TAG, "Failed to sort artists", e);
            }
        }

        public class ArtistComparator : IComparer<Artist>
        {
            private readonly string[] _ignoredArticles;

            public ArtistComparator(string[] ignoredArticles)
            {
                _ignoredArticles = ignoredArticles;
            }

            public int Compare(Artist lhsArtist, Artist rhsArtist)
            {
                var lhs = lhsArtist.GetName().ToLower();
                var rhs = rhsArtist.GetName().ToLower();

                foreach (var article in _ignoredArticles)
                {
                    var index = lhs.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
                    if (index == 0)
                        lhs = lhs.Substring(article.Length + 1);
                    index = rhs.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
                    if (index == 0)
                        rhs = rhs.Substring(article.Length + 1);
                }

                return string.CompareOrdinal(lhs, rhs);
            }
        }
    }
}