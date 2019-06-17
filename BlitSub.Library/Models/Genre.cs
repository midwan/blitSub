using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    public class Genre
    {
        public int AlbumCount { get; set; }
        public string Index { get; set; }
        public string Name { get;set; }
        public int SongCount { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
