using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Genre
    {
        private int albumCount;
        private string index;

        private string name;
        private int songCount;

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

        public override string ToString()
        {
            return name;
        }

        public int getAlbumCount()
        {
            return albumCount;
        }

        public void setAlbumCount(int albumCount)
        {
            this.albumCount = albumCount;
        }

        public int getSongCount()
        {
            return songCount;
        }

        public void setSongCount(int songCount)
        {
            this.songCount = songCount;
        }

        public static class GenreComparator : Comparator<Genre>
        {
            public override int Compare(Genre genre1, Genre genre2)
            {
                return genre1.getName().compareToIgnoreCase(genre2.getName());
            }

            public static List<Genre> sort(List<Genre> genres)
            {
                Collections.sort(genres, new GenreComparator());
                return genres;
            }
        }
    }
}