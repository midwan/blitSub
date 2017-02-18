using System;
using System.Collections.Generic;
using System.IO;
using blitSub.Domain.DTO;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class MusicDirectory
    {
        private static string TAG;
        private List<Entry> _children;
        private string _id;

        private string _name;
        private string _parent;

        public MusicDirectory()
        {
            _children = new List<Entry>();
            TAG = GetName();
        }

        public MusicDirectory(List<Entry> children)
        {
            this._children = children;
            TAG = GetName();
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            this._name = name;
        }

        public string GetId()
        {
            return _id;
        }

        public void SetId(string id)
        {
            this._id = id;
        }

        public string GetParent()
        {
            return _parent;
        }

        public void SetParent(string parent)
        {
            this._parent = parent;
        }

        public void AddChild(Entry child)
        {
            if (child != null)
            {
                _children.Add(child);
            }
        }

        public void AddChildren(List<Entry> children)
        {
            this._children.AddRange(children);
        }

        public void ReplaceChildren(List<Entry> children)
        {
            this._children = children;
        }

        public List<Entry> GetChildren()
        {
            TAG = GetName();
            return GetChildren(true, true);
        }

        public List<Entry> GetChildren(bool includeDirs, bool includeFiles)
        {
            if (includeDirs && includeFiles)
            {
                return _children;
            }

            List<Entry> result = new List<Entry>(_children.Count);
            foreach (Entry child in _children)
            {
                if (child != null && child.IsDirectory() && includeDirs || !child.IsDirectory() && includeFiles)
                {
                    result.Add(child);
                }
            }
            return result;
        }

        public List<Entry> GetSongs()
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry child in _children)
            {
                if (child != null && !child.IsDirectory() && !child.IsVideo())
                {
                    result.Add(child);
                }
            }
            return result;
        }

        public int GetChildrenSize()
        {
            return _children.Count;
        }

        public void ShuffleChildren()
        {
            Collections.shuffle(_children);
        }

        public void SortChildren(Context context, int instance)
        {
            // Only apply sorting on server version 4.7 and greater, where disc is supported
            if (ServerInfo.checkServerVersion(context, "1.8", instance))
            {
                SortChildren(Util.getPreferences(context)
                    .getBoolean(Constants.PREFERENCES_KEY_CUSTOM_SORT_ENABLED, true));
            }
        }

        public void SortChildren(bool byYear)
        {
            EntryComparator.Sort(_children, byYear);
        }

        public bool UpdateMetadata(MusicDirectory refreshedDirectory)
        {
            bool metadataUpdated = false;
            foreach (var entry in _children)
            {
                int index = refreshedDirectory._children.IndexOf(entry);
                if (index != -1)
                {
                    var refreshed = refreshedDirectory._children[index];

                    entry.SetTitle(refreshed.GetTitle());
                    entry.SetAlbum(refreshed.GetAlbum());
                    entry.SetArtist(refreshed.GetArtist());
                    entry.SetTrack(refreshed.GetTrack());
                    entry.SetYear(refreshed.GetYear());
                    entry.SetGenre(refreshed.GetGenre());
                    entry.SetTranscodedContentType(refreshed.GetTranscodedContentType());
                    entry.SetTranscodedSuffix(refreshed.GetTranscodedSuffix());
                    entry.SetDiscNumber(refreshed.GetDiscNumber());
                    entry.SetStarred(refreshed.IsStarred());
                    entry.SetRating(refreshed.GetRating());
                    entry.SetType(refreshed.GetType());
                    if (!Util.equals(entry.GetCoverArt(), refreshed.GetCoverArt()))
                    {
                        metadataUpdated = true;
                        entry.SetCoverArt(refreshed.GetCoverArt());
                    }
//TODO:
                    //    new UpdateHelper.EntryInstanceUpdater(entry) {

                    //    public void Update(Entry found)
                    //    {
                    //        found.setTitle(refreshed.getTitle());
                    //        found.setAlbum(refreshed.getAlbum());
                    //        found.setArtist(refreshed.getArtist());
                    //        found.setTrack(refreshed.getTrack());
                    //        found.setYear(refreshed.getYear());
                    //        found.setGenre(refreshed.getGenre());
                    //        found.setTranscodedContentType(refreshed.getTranscodedContentType());
                    //        found.setTranscodedSuffix(refreshed.getTranscodedSuffix());
                    //        found.setDiscNumber(refreshed.getDiscNumber());
                    //        found.setStarred(refreshed.isStarred());
                    //        found.setRating(refreshed.getRating());
                    //        found.setType(refreshed.getType());
                    //        if (!Util.equals(found.getCoverArt(), refreshed.getCoverArt()))
                    //        {
                    //            found.setCoverArt(refreshed.getCoverArt());
                    //            metadataUpdate = DownloadService.METADATA_UPDATED_COVER_ART;
                    //        }
                    //    }
                    //}.Execute();
                }
            }

            return metadataUpdated;
        }

        public bool UpdateEntriesList(Context context, int instance, MusicDirectory refreshedDirectory)
        {
            bool changed = false;
            foreach (var entry in _children)
            {
                // No longer exists in here
                if (refreshedDirectory._children.IndexOf(entry) == -1)
                {
                    _children.Remove(entry);
                    changed = true;
                }
            }

            // Make sure we contain all children from refreshed set
            bool resort = false;
            foreach (var refreshed in refreshedDirectory._children)
            {
                if (!_children.Contains(refreshed))
                {
                    _children.Add(refreshed);
                    resort = true;
                    changed = true;
                }
            }

            if (resort)
            {
                SortChildren(context, instance);
            }

            return changed;
        }

        [Serializable]
        public class Entry
        {
            public static readonly int TYPE_SONG = 0;
            public static readonly int TYPE_PODCAST = 1;
            public static readonly int TYPE_AUDIO_BOOK = 2;

            private string id;
            private string parent;
            private string grandParent;
            private string albumId;
            private string artistId;
            private bool directory;
            private string title;
            private string album;
            private string artist;
            private int track;
            private int customOrder;
            private int year;
            private string genre;
            private string contentType;
            private string suffix;
            private string transcodedContentType;
            private string transcodedSuffix;
            private string coverArt;
            private long size;
            private int duration;
            private int bitRate;
            private string path;
            private bool video;
            private int discNumber;
            private bool starred;
            private int rating;
            private Bookmark bookmark;
            private int type = 0;
            private int closeness;
            //[NotSerialized]
            private Artist linkedArtist;

		public Entry()
            {

            }
            public Entry(string id)
            {
                this.id = id;
            }
            public Entry(Artist artist)
            {
                this.id = artist.GetId();
                this.title = artist.GetName();
                this.directory = true;
                this.starred = artist.IsStarred();
                this.rating = artist.GetRating();
                this.linkedArtist = artist;
            }

        public void LoadMetadata(File file)
            {
                try
                {
                    MediaMetadataRetriever metadata = new MediaMetadataRetriever();
                    metadata.setDataSource(file.getAbsolutePath());
                    String discNumber = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_DISC_NUMBER);
                    if (discNumber == null)
                    {
                        discNumber = "1/1";
                    }
                    int slashIndex = discNumber.IndexOf("/");
                    if (slashIndex > 0)
                    {
                        discNumber = discNumber.Substring(0, slashIndex);
                    }
                    try
                    {
                        SetDiscNumber(int.Parse(discNumber));
                    }
                    catch (Exception e)
                    {
                        Log.w(TAG, "Non numbers in disc field!");
                    }
                    String bitrate = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_BITRATE);
                    SetBitRate(int.Parse(bitrate ?? "0") / 1000);
                    String length = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_DURATION);
                    SetDuration(int.Parse(length) / 1000);
                    String artist = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_ARTIST);
                    if (artist != null)
                    {
                        SetArtist(artist);
                    }
                    String album = metadata.extractMetadata(MediaMetadataRetriever.METADATA_KEY_ALBUM);
                    if (album != null)
                    {
                        SetAlbum(album);
                    }
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
                    String filename = GetPath();
                    if (filename == null)
                    {
                        return;
                    }

                    int index = filename.LastIndexOf('/');
                    if (index != -1)
                    {
                        filename = filename.Substring(index + 1);
                        if (GetTrack() != null)
                        {
                            filename = filename.Replace(string.Format("%02d ", GetTrack()), "");
                        }

                        index = filename.LastIndexOf('.');
                        if (index != -1)
                        {
                            filename = filename.Substring(0, index);
                        }

                        SetTitle(filename);
                    }
                }
                catch (Exception e)
                {
                    Log.w(TAG, "Failed to update title based off of path", e);
                }
            }

            public string GetId()
            {
                return id;
            }

            public void SetId(string id)
            {
                this.id = id;
            }

            private string GetParent()
            {
                return parent;
            }

            public void SetParent(string parent)
            {
                this.parent = parent;
            }

            public string GetGrandParent()
            {
                return grandParent;
            }

            public void SetGrandParent(string grandParent)
            {
                this.grandParent = grandParent;
            }

            public string GetAlbumId()
            {
                return albumId;
            }

            public void SetAlbumId(string albumId)
            {
                this.albumId = albumId;
            }

            public string GetArtistId()
            {
                return artistId;
            }

            public void SetArtistId(string artistId)
            {
                this.artistId = artistId;
            }

            public bool IsDirectory()
            {
                return directory;
            }

            public void SetDirectory(bool directory)
            {
                this.directory = directory;
            }

            public string GetTitle()
            {
                return title;
            }

            public void SetTitle(string title)
            {
                this.title = title;
            }

            public string GetAlbum()
            {
                return album;
            }

            public bool IsAlbum()
            {
                return GetParent() != null || GetArtist() != null;
            }

            public string GetAlbumDisplay()
            {
                if (album != null && title.StartsWith("Disc "))
                {
                    return album;
                }
                else
                {
                    return title;
                }
            }

            public void SetAlbum(string album)
            {
                this.album = album;
            }

            public string GetArtist()
            {
                return artist;
            }

            public void SetArtist(string artist)
            {
                this.artist = artist;
            }

            public int GetTrack()
            {
                return track;
            }

            public void SetTrack(int track)
            {
                this.track = track;
            }

            public int GetCustomOrder()
            {
                return customOrder;
            }
            public void SetCustomOrder(int customOrder)
            {
                this.customOrder = customOrder;
            }

            public int GetYear()
            {
                return year;
            }

            public void SetYear(int year)
            {
                this.year = year;
            }

            public string GetGenre()
            {
                return genre;
            }

            public void SetGenre(string genre)
            {
                this.genre = genre;
            }

            public string GetContentType()
            {
                return contentType;
            }

            public void SetContentType(string contentType)
            {
                this.contentType = contentType;
            }

            public string GetSuffix()
            {
                return suffix;
            }

            public void SetSuffix(string suffix)
            {
                this.suffix = suffix;
            }

            public string GetTranscodedContentType()
            {
                return transcodedContentType;
            }

            public void SetTranscodedContentType(string transcodedContentType)
            {
                this.transcodedContentType = transcodedContentType;
            }

            public string GetTranscodedSuffix()
            {
                return transcodedSuffix;
            }

            public void SetTranscodedSuffix(string transcodedSuffix)
            {
                this.transcodedSuffix = transcodedSuffix;
            }

            public long GetSize()
            {
                return size;
            }

            public void SetSize(long size)
            {
                this.size = size;
            }

            public int GetDuration()
            {
                return duration;
            }

            public void SetDuration(int duration)
            {
                this.duration = duration;
            }

            public int GetBitRate()
            {
                return bitRate;
            }

            public void SetBitRate(int bitRate)
            {
                this.bitRate = bitRate;
            }

            public string GetCoverArt()
            {
                return coverArt;
            }

            public void SetCoverArt(string coverArt)
            {
                this.coverArt = coverArt;
            }

            public string GetPath()
            {
                return path;
            }

            public void SetPath(string path)
            {
                this.path = path;
            }

            public bool IsVideo()
            {
                return video;
            }

            public void SetVideo(bool video)
            {
                this.video = video;
            }

            public int GetDiscNumber()
            {
                return discNumber;
            }

            public void SetDiscNumber(int discNumber)
            {
                this.discNumber = discNumber;
            }

            public bool IsStarred()
            {
                return starred;
            }

            public void SetStarred(bool starred)
            {
                this.starred = starred;

                if (linkedArtist != null)
                {
                    linkedArtist.SetStarred(starred);
                }
            }

            public int GetRating()
            {
                return rating == null ? 0 : rating;
            }
            public void SetRating(int? rating)
            {
                if (rating == null || rating == 0)
                {
                    this.rating = null;
                }
                else
                {
                    this.rating = rating;
                }

                if (linkedArtist != null)
                {
                    linkedArtist.SetRating(rating);
                }
            }

            public Bookmark GetBookmark()
            {
                return bookmark;
            }
            public void SetBookmark(Bookmark bookmark)
            {
                this.bookmark = bookmark;
            }

            public int GetType()
            {
                return type;
            }
            public void SetType(int type)
            {
                this.type = type;
            }
            public bool IsSong()
            {
                return type == TYPE_SONG;
            }
            public bool IsPodcast()
            {
                return this is PodcastEpisode || type == TYPE_PODCAST;
            }
            public bool IsAudioBook()
            {
                return type == TYPE_AUDIO_BOOK;
            }

            public int GetCloseness()
            {
                return closeness;
            }

            public void SetCloseness(int closeness)
            {
                this.closeness = closeness;
            }

            public bool IsOnlineId(Context context)
            {
                try
                {
                    string cacheLocation = Util.getPreferences(context).getString(Constants.PREFERENCES_KEY_CACHE_LOCATION, null);
                    return cacheLocation == null || id == null || id.IndexOf(cacheLocation, StringComparison.Ordinal) == -1;
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
                {
                    return true;
                }
                if (o == null || base.GetType() != o.GetType())
                {
                    return false;
                }

                Entry entry = (Entry)o;
                return id.Equals(entry.id);
            }

        public override int GetHashCode()
            {
                return id.GetHashCode();
            }

        public override string ToString()
            {
                return title;
            }
        }

        public class EntryComparator : IComparer<Entry> {

        private bool byYear;

        public EntryComparator(bool byYear)
        {
            this.byYear = byYear;
        }

        public int Compare(Entry lhs, Entry rhs)
        {
            if (lhs.IsDirectory() && !rhs.IsDirectory())
            {
                return -1;
            }
            else if (!lhs.IsDirectory() && rhs.IsDirectory())
            {
                return 1;
            }
            else if (lhs.IsDirectory() && rhs.IsDirectory())
            {
                if (byYear)
                {
                    int lhsYear = lhs.GetYear();
                    int rhsYear = rhs.GetYear();
                    if (lhsYear != null && rhsYear != null)
                    {
                        return lhsYear.CompareTo(rhsYear);
                    }
                    else if (lhsYear != null)
                    {
                        return -1;
                    }
                    else if (rhsYear != null)
                    {
                        return 1;
                    }
                }

                return string.CompareOrdinal(lhs.GetAlbumDisplay(), rhs.GetAlbumDisplay());
            }

            int lhsDisc = lhs.GetDiscNumber();
            int rhsDisc = rhs.GetDiscNumber();

            if (lhsDisc != null && rhsDisc != null)
            {
                if (lhsDisc < rhsDisc)
                {
                    return -1;
                }
                else if (lhsDisc > rhsDisc)
                {
                    return 1;
                }
            }

            int lhsTrack = lhs.GetTrack();
            int rhsTrack = rhs.GetTrack();
            if (lhsTrack != null && rhsTrack != null && lhsTrack != rhsTrack)
            {
                return lhsTrack.CompareTo(rhsTrack);
            }
            else if (lhsTrack != null)
            {
                return -1;
            }
            else if (rhsTrack != null)
            {
                return 1;
            }

            return string.CompareOrdinal(lhs.GetTitle(), rhs.GetTitle());
        }

        public static void Sort(List<Entry> entries)
        {
            Sort(entries, true);
        }
        public static void Sort(List<Entry> entries, bool byYear)
        {
            try
            {
                entries.Sort(new EntryComparator(byYear));
            }
            catch (Exception e)
            {
                Log.w(TAG, "Failed to sort MusicDirectory");
            }
        }
    }
}
}