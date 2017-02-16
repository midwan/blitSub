using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class MusicDirectory
    {
        private static readonly string TAG = MusicDirectory.getSimpleName();
        private List<Entry> children;
        private string id;

        private string name;
        private string parent;

        public MusicDirectory()
        {
            children = new ArrayList<Entry>();
        }

        public MusicDirectory(List<Entry> children)
        {
            this.children = children;
        }

        private getChildren()
        {
            return getChildren(true, true);
        }

        private getChildren(boolean includeDirs, boolean includeFiles)
        {
            if (includeDirs && includeFiles)
                return children;

            List<Entry> result = new ArrayList<Entry>(children.size());
            foreach (var child in children)
                if (child != null && child.isDirectory() && includeDirs || !child.isDirectory() && includeFiles)
                    result.Add(child);
            return result;
        }

        private getSongs()
        {
            List<Entry> result = new ArrayList<Entry>();
            foreach (var child in children)
                if (child != null && !child.isDirectory() && !child.isVideo())
                    result.Add(child);
            return result;
        }

        private updateMetadata(MusicDirectory refreshedDirectory)
        {
            var metadataUpdated = false;
            Iterator<Entry> it = children.iterator();
            while (it.hasNext())
            {
                Entry entry = it.next();
                int index = refreshedDirectory.children.indexOf(entry);
                if (index != -1)
                {
                    final
                    Entry refreshed = refreshedDirectory.children.get(index);

                    entry.setTitle(refreshed.getTitle());
                    entry.setAlbum(refreshed.getAlbum());
                    entry.setArtist(refreshed.getArtist());
                    entry.setTrack(refreshed.getTrack());
                    entry.setYear(refreshed.getYear());
                    entry.setGenre(refreshed.getGenre());
                    entry.setTranscodedContentType(refreshed.getTranscodedContentType());
                    entry.setTranscodedSuffix(refreshed.getTranscodedSuffix());
                    entry.setDiscNumber(refreshed.getDiscNumber());
                    entry.setStarred(refreshed.isStarred());
                    entry.setRating(refreshed.getRating());
                    entry.setType(refreshed.getType());
                    if (!Util.equals(entry.getCoverArt(), refreshed.getCoverArt()))
                    {
                        metadataUpdated = true;
                        entry.setCoverArt(refreshed.getCoverArt());
                    }

                    new UpdateHelper.EntryInstanceUpdater(entry)
                    {
                        Override

                        public void update(Entry found)
                        {
                        found.setTitle(refreshed.getTitle());
                        found.setAlbum(refreshed.getAlbum());
                        found.setArtist(refreshed.getArtist());
                        found.setTrack(refreshed.getTrack());
                        found.setYear(refreshed.getYear());
                        found.setGenre(refreshed.getGenre());
                        found.setTranscodedContentType(refreshed.getTranscodedContentType());
                        found.setTranscodedSuffix(refreshed.getTranscodedSuffix());
                        found.setDiscNumber(refreshed.getDiscNumber());
                        found.setStarred(refreshed.isStarred());
                        found.setRating(refreshed.getRating());
                        found.setType(refreshed.getType());
                        if (!Util.equals(found.getCoverArt(),
                        refreshed.getCoverArt()))
                        {
                        found.setCoverArt(refreshed.getCoverArt());
                        metadataUpdate = DownloadService.METADATA_UPDATED_COVER_ART;
                    }
                    }
                    }
                    .
                    execute();
                }
            }

            return metadataUpdated;
        }

        private updateEntriesList(Context context, int instance, MusicDirectory refreshedDirectory)
        {
            var changed = false;
            Iterator<Entry> it = children.iterator();
            while (it.hasNext())
            {
                Entry entry = it.next();
                // No longer exists in here
                if (refreshedDirectory.children.IndexOf(entry) == -1)
                {
                    it.remove();
                    changed = true;
                }
            }

            // Make sure we contain all children from refreshed set
            var resort = false;
            foreach (var refreshed in refreshedDirectory.children)
                if (!children.Contains(refreshed))
                {
                    children.Add(refreshed);
                    resort = true;
                    changed = true;
                }

            if (resort)
                sortChildren(context, instance);

            return changed;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
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

        public string getParent()
        {
            return parent;
        }

        public void setParent(string parent)
        {
            this.parent = parent;
        }

        public void addChild(Entry child)
        {
            if (child != null)
                children.Add(child);
        }

        public void addChildren(List<Entry> children)
        {
            this.children.AddRange(children);
        }

        public void replaceChildren(List<Entry> children)
        {
            this.children = children;
        }

        public synchronized List<Entry> 

        public synchronized List<Entry> 
        public synchronized List<Entry> 

        public synchronized 

        private int getChildrenSize()
        {
            return children.size();
        }

        public void shuffleChildren()
        {
            Collections.shuffle(children);
        }

        public void sortChildren(Context context, int instance)
        {
            // Only apply sorting on server version 4.7 and greater, where disc is supported
            if (ServerInfo.checkServerVersion(context, "1.8", instance))
                sortChildren(Util.getPreferences(context)
                    .getBoolean(Constants.PREFERENCES_KEY_CUSTOM_SORT_ENABLED, true));
        }

        public void sortChildren(bool byYear)
        {
            EntryComparator.sort(children, byYear);
        }

        public synchronized boolean
        public synchronized boolean

        [Serializable]
        public static class Entry
        {
            public static int TYPE_SONG = 0;
            public static int TYPE_PODCAST = 1;
            public static int TYPE_AUDIO_BOOK = 2;
            private string album;
            private string albumId;
            private string artist;
            private string artistId;
            private int bitRate;
            private Bookmark bookmark;
            private int closeness;
            private string contentType;
            private string coverArt;
            private int customOrder;
            private bool directory;
            private int discNumber;
            private int duration;
            private string genre;
            private string grandParent;

            private string id;
            private string parent;
            private string path;
            private int rating;
            private long size;
            private bool starred;
            private string suffix;
            private string title;
            private int track;
            private string transcodedContentType;
            private string transcodedSuffix;
            private int type;
            private bool video;
            private int year;

            public Entry()
            {
            }

            public Entry(string id)
            {
                this.id = id;
            }

            public Entry(Artist artist)
            {
                id = artist.getId();
                title = artist.getName();
                directory = true;
                starred = artist.isStarred();
                rating = artist.getRating();
                this.linkedArtist = artist;
            }


            private TargetApi(Build.VERSION_CODES.GINGERBREAD_MR1 )
            private transient Artist 
            linkedArtist;

            public void loadMetadata(File file)
            {
                try
                {
                    MediaMetadataRetriever metadata = new MediaMetadataRetriever();
                    metadata.setDataSource(file.getAbsolutePath());
                    string discNumber = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_DISC_NUMBER);
                    if (discNumber == null)
                        discNumber = "1/1";
                    var slashIndex = discNumber.IndexOf("/");
                    if (slashIndex > 0)
                        discNumber = discNumber.Substring(0, slashIndex);
                    try
                    {
                        setDiscNumber(int.Parse(discNumber));
                    }
                    catch (Exception e)
                    {
                        Log.w(TAG, "Non numbers in disc field!");
                    }
                    string bitrate = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_BITRATE);
                    setBitRate(int.Parse(bitrate != null ? bitrate : "0") / 1000);
                    string length = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_DURATION);
                    setDuration(int.Parse(length) / 1000);
                    string artist = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_ARTIST);
                    if (artist != null)
                        setArtist(artist);
                    string album = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_ALBUM);
                    if (album != null)
                        setAlbum(album);
                    metadata.release();
                }
                catch (Exception e)
                {
                    Log.i(TAG, "Device doesn't properly support MediaMetadataRetreiver", e);
                }
            }

            public void rebaseTitleOffPath()
            {
                try
                {
                    var filename = getPath();
                    if (filename == null)
                        return;

                    var index = filename.LastIndexOf('/');
                    if (index != -1)
                    {
                        filename = filename.Substring(index + 1);
                        if (getTrack() != null)
                            filename = filename.Replace(string.Format("%02d ", getTrack()), "");

                        index = filename.LastIndexOf('.');
                        if (index != -1)
                            filename = filename.Substring(0, index);

                        setTitle(filename);
                    }
                }
                catch (Exception e)
                {
                    Log.w(TAG, "Failed to update title based off of path", e);
                }
            }

            public string getId()
            {
                return id;
            }

            public void setId(string id)
            {
                this.id = id;
            }

            public string getParent()
            {
                return parent;
            }

            public void setParent(string parent)
            {
                this.parent = parent;
            }

            public string getGrandParent()
            {
                return grandParent;
            }

            public void setGrandParent(string grandParent)
            {
                this.grandParent = grandParent;
            }

            public string getAlbumId()
            {
                return albumId;
            }

            public void setAlbumId(string albumId)
            {
                this.albumId = albumId;
            }

            public string getArtistId()
            {
                return artistId;
            }

            public void setArtistId(string artistId)
            {
                this.artistId = artistId;
            }

            public bool isDirectory()
            {
                return directory;
            }

            public void setDirectory(bool directory)
            {
                this.directory = directory;
            }

            public string getTitle()
            {
                return title;
            }

            public void setTitle(string title)
            {
                this.title = title;
            }

            public string getAlbum()
            {
                return album;
            }

            public bool isAlbum()
            {
                return getParent() != null || getArtist() != null;
            }

            public string getAlbumDisplay()
            {
                if (album != null && title.StartsWith("Disc "))
                    return album;
                return title;
            }

            public void setAlbum(string album)
            {
                this.album = album;
            }

            public string getArtist()
            {
                return artist;
            }

            public void setArtist(string artist)
            {
                this.artist = artist;
            }

            public int getTrack()
            {
                return track;
            }

            public void setTrack(int track)
            {
                this.track = track;
            }

            public int getCustomOrder()
            {
                return customOrder;
            }

            public void setCustomOrder(int customOrder)
            {
                this.customOrder = customOrder;
            }

            public int getYear()
            {
                return year;
            }

            public void setYear(int year)
            {
                this.year = year;
            }

            public string getGenre()
            {
                return genre;
            }

            public void setGenre(string genre)
            {
                this.genre = genre;
            }

            public string getContentType()
            {
                return contentType;
            }

            public void setContentType(string contentType)
            {
                this.contentType = contentType;
            }

            public string getSuffix()
            {
                return suffix;
            }

            public void setSuffix(string suffix)
            {
                this.suffix = suffix;
            }

            public string getTranscodedContentType()
            {
                return transcodedContentType;
            }

            public void setTranscodedContentType(string transcodedContentType)
            {
                this.transcodedContentType = transcodedContentType;
            }

            public string getTranscodedSuffix()
            {
                return transcodedSuffix;
            }

            public void setTranscodedSuffix(string transcodedSuffix)
            {
                this.transcodedSuffix = transcodedSuffix;
            }

            public long getSize()
            {
                return size;
            }

            public void setSize(long size)
            {
                this.size = size;
            }

            public int getDuration()
            {
                return duration;
            }

            public void setDuration(int duration)
            {
                this.duration = duration;
            }

            public int getBitRate()
            {
                return bitRate;
            }

            public void setBitRate(int bitRate)
            {
                this.bitRate = bitRate;
            }

            public string getCoverArt()
            {
                return coverArt;
            }

            public void setCoverArt(string coverArt)
            {
                this.coverArt = coverArt;
            }

            public string getPath()
            {
                return path;
            }

            public void setPath(string path)
            {
                this.path = path;
            }

            public bool isVideo()
            {
                return video;
            }

            public void setVideo(bool video)
            {
                this.video = video;
            }

            public int getDiscNumber()
            {
                return discNumber;
            }

            public void setDiscNumber(int discNumber)
            {
                this.discNumber = discNumber;
            }

            public bool isStarred()
            {
                return starred;
            }

            public void setStarred(bool starred)
            {
                this.starred = starred;

                if (linkedArtist != null)
                    linkedArtist.setStarred(starred);
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

                if (linkedArtist != null)
                    linkedArtist.setRating(rating);
            }

            public Bookmark getBookmark()
            {
                return bookmark;
            }

            public void setBookmark(Bookmark bookmark)
            {
                this.bookmark = bookmark;
            }

            public int getType()
            {
                return type;
            }

            public void setType(int type)
            {
                this.type = type;
            }

            public bool isSong()
            {
                return type == TYPE_SONG;
            }

            public bool isPodcast()
            {
                return this
                instanceof
                PodcastEpisode || type == TYPE_PODCAST;
            }

            public bool isAudioBook()
            {
                return type == TYPE_AUDIO_BOOK;
            }

            public int getCloseness()
            {
                return closeness;
            }

            public void setCloseness(int closeness)
            {
                this.closeness = closeness;
            }

            public bool isOnlineId(Context context)
            {
                try
                {
                    string cacheLocation =
                        Util.getPreferences(context).getString(Constants.PREFERENCES_KEY_CACHE_LOCATION, null);
                    return cacheLocation == null || id == null || id.indexOf(cacheLocation) == -1;
                }
                catch (Exception e)
                {
                    Log.w(TAG, "Failed to check online id validity");

                    // Err on the side of default functionality
                    return true;
                }
            }

            public override bool Equals(object o)
            {
                if (this == o)
                    return true;
                if (o == null || getClass() != o.getClass())
                    return false;

                var entry = (Entry) o;
                return id.equals(entry.id);
            }

            public override int HashCode()
            {
                return id.hashCode();
            }

            public override string ToString()
            {
                return title;
            }
        }

        public static class EntryComparator : Comparator<Entry>
        {
            private readonly boolean byYear;
            private readonly Collator collator;

            public EntryComparator(bool byYear)
            {
                this.byYear = byYear;
                collator = Collator.getInstance(Locale.US);
                collator.setStrength(Collator.PRIMARY);
            }

            public int compare(Entry lhs, Entry rhs)
            {
                if (lhs.isDirectory() && !rhs.isDirectory())
                    return -1;
                if (!lhs.isDirectory() && rhs.isDirectory())
                    return 1;
                if (lhs.isDirectory() && rhs.isDirectory())
                {
                    if (byYear)
                    {
                        var lhsYear = lhs.getYear();
                        var rhsYear = rhs.getYear();
                        if (lhsYear != null && rhsYear != null)
                            return lhsYear.CompareTo(rhsYear);
                        if (lhsYear != null)
                            return -1;
                        if (rhsYear != null)
                            return 1;
                    }

                    return collator.compare(lhs.getAlbumDisplay(), rhs.getAlbumDisplay());
                }

                var lhsDisc = lhs.getDiscNumber();
                var rhsDisc = rhs.getDiscNumber();

                if (lhsDisc != null && rhsDisc != null)
                    if (lhsDisc < rhsDisc)
                        return -1;
                    else if (lhsDisc > rhsDisc)
                        return 1;

                var lhsTrack = lhs.getTrack();
                var rhsTrack = rhs.getTrack();
                if (lhsTrack != null && rhsTrack != null && lhsTrack != rhsTrack)
                    return lhsTrack.CompareTo(rhsTrack);
                if (lhsTrack != null)
                    return -1;
                if (rhsTrack != null)
                    return 1;

                return collator.compare(lhs.getTitle(), rhs.getTitle());
            }

            public static void sort(List<Entry> entries)
            {
                sort(entries, true);
            }

            public static void sort(List<Entry> entries, bool byYear)
            {
                try
                {
                    Collections.sort(entries, new EntryComparator(byYear));
                }
                catch (Exception e)
                {
                    Log.w(TAG, "Failed to sort MusicDirectory");
                }
            }
        }
    }
}