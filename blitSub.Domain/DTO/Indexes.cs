using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Indexes
    {
        private readonly List<Artist> artists;
        private readonly List<MusicDirectory.Entry> entries;

        private readonly long lastModified;
        private List<Artist> shortcuts;

        public Indexes()
        {
        }

        public Indexes(long lastModified, List<Artist> shortcuts, List<Artist> artists)
        {
            this.lastModified = lastModified;
            this.shortcuts = shortcuts;
            this.artists = artists;
            entries = new ArrayList<MusicDirectory.Entry>();
        }

        public Indexes(long lastModified, List<Artist> shortcuts, List<Artist> artists,
            List<MusicDirectory.Entry> entries)
        {
            this.lastModified = lastModified;
            this.shortcuts = shortcuts;
            this.artists = artists;
            this.entries = entries;
        }

        public long getLastModified()
        {
            return lastModified;
        }

        public List<Artist> getShortcuts()
        {
            return shortcuts;
        }

        public List<Artist> getArtists()
        {
            return artists;
        }

        public void setArtists(List<Artist> artists)
        {
            shortcuts = new ArrayList<Artist>();
            this.artists.Clear();
            this.artists.AddRange(artists);
        }

        public List<MusicDirectory.Entry> getEntries()
        {
            return entries;
        }

        public void sortChildren(Context context)
        {
            SharedPreferences prefs = Util.getPreferences(context);
            string ignoredArticlesString = prefs.getString(Constants.CACHE_KEY_IGNORE, "The El La Los Las Le Les");
            final
            string[] ignoredArticles = ignoredArticlesString.split(" ");

            Artist.sort(shortcuts, ignoredArticles);
            Artist.sort(artists, ignoredArticles);
        }
    }
}