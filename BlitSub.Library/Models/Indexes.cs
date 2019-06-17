using System;
using System.Collections.Generic;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Indexes
    {
        public List<Artist> Artists;
        public List<MusicDirectory.Entry> Entries;
        public List<Artist> Shortcuts;

        public Indexes()
        {
        }

        public Indexes(long lastModified, List<Artist> shortcuts, List<Artist> artists)
        {
            LastModified = lastModified;
            Shortcuts = shortcuts;
            Artists = artists;
            Entries = new List<MusicDirectory.Entry>();
        }

        public Indexes(long lastModified, List<Artist> shortcuts, List<Artist> artists,
            List<MusicDirectory.Entry> entries)
        {
            LastModified = lastModified;
            Shortcuts = shortcuts;
            Artists = artists;
            Entries = entries;
        }

        public long LastModified { get; set; }
    }
}