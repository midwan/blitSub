using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Genre
    {
        private int _albumCount;
        private string _index;

        private string _name;
        private int _songCount;

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

        public override string ToString()
        {
            return _name;
        }

        public int GetAlbumCount()
        {
            return _albumCount;
        }

        public void SetAlbumCount(int albumCount)
        {
            _albumCount = albumCount;
        }

        public int GetSongCount()
        {
            return _songCount;
        }

        public void SetSongCount(int songCount)
        {
            _songCount = songCount;
        }

        public class GenreComparator : IComparer<Genre>
        {
            public int Compare(Genre genre1, Genre genre2)
            {
                return string.Compare(genre1.GetName(), genre2.GetName(), StringComparison.Ordinal);
            }

            public static List<Genre> Sort(List<Genre> genres)
            {
                genres.Sort(new GenreComparator());
                return genres;
            }
        }
    }
}