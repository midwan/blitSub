using System;
using System.Collections.Generic;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class Indexes
    {
        private readonly List<Artist> _artists;
        private readonly List<MusicDirectory.Entry> _entries;

        private readonly long _lastModified;
        private List<Artist> _shortcuts;

        public Indexes()
        {
        }

        public Indexes(long lastModified, List<Artist> shortcuts, List<Artist> artists)
        {
            _lastModified = lastModified;
            _shortcuts = shortcuts;
            _artists = artists;
            _entries = new List<MusicDirectory.Entry>();
        }

        public Indexes(long lastModified, List<Artist> shortcuts, List<Artist> artists,
            List<MusicDirectory.Entry> entries)
        {
            _lastModified = lastModified;
            _shortcuts = shortcuts;
            _artists = artists;
            _entries = entries;
        }

        public long GetLastModified()
        {
            return _lastModified;
        }

        public List<Artist> GetShortcuts()
        {
            return _shortcuts;
        }

        public List<Artist> GetArtists()
        {
            return _artists;
        }

        public void SetArtists(List<Artist> artists)
        {
            _shortcuts = new List<Artist>();
            _artists.Clear();
            _artists.AddRange(artists);
        }

        public List<MusicDirectory.Entry> GetEntries()
        {
            return _entries;
        }

        public void SortChildren(Context context)
        {
            SharedPreferences prefs = Util.getPreferences(context);
            string ignoredArticlesString = prefs.getString(Constants.CACHE_KEY_IGNORE, "The El La Los Las Le Les");
            var ignoredArticles = ignoredArticlesString.Split(" ");

            Artist.Sort(_shortcuts, ignoredArticles);
            Artist.Sort(_artists, ignoredArticles);
        }
    }
}