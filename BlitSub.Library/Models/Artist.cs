using System;
using System.Collections.Generic;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Artist
    {
        public const string ROOT_ID = "-1";
        public const string MISSING_ID = "-2";
        public readonly string TAG;

        public Artist()
        {
        }

        public Artist(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Closeness { get; set; }
        public string Id { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public bool Starred { get; set; }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (o == null || GetType() != o.GetType())
                return false;

            var entry = (Artist) o;
            return Id.Equals(entry.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public static void Sort(List<Artist> artists, string[] ignoredArticles)
        {
            try
            {
                artists.Sort(new ArtistComparator(ignoredArticles));
            }
            catch (Exception e)
            {
                // TODO Log function
                //Log.w(TAG, "Failed to sort artists", e);
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
                var lhs = lhsArtist?.Name.ToLower();
                var rhs = rhsArtist?.Name.ToLower();

                foreach (var article in _ignoredArticles)
                {
                    var index = lhs?.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
                    if (index == 0)
                        lhs = lhs.Substring(article.Length + 1);
                    index = rhs?.IndexOf(article.ToLower() + " ", StringComparison.Ordinal);
                    if (index == 0)
                        rhs = rhs.Substring(article.Length + 1);
                }

                return string.CompareOrdinal(lhs, rhs);
            }
        }
    }
}