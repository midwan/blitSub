using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class MusicDirectory
    {
        public List<Entry> Children { get; set; }
        public string Id { get;set; }
        public string Name { get; set; }
        public string Parent { get;set; }

        public MusicDirectory()
        {
            Children = new List<Entry>();
        }

        public MusicDirectory(List<Entry> children)
        {
            Children = children;
        }

        public List<Entry> GetChildren(bool includeDirs = true, bool includeFiles = true)
        {
            if (includeDirs && includeFiles)
            {
                return Children;
            }

            var result = new List<Entry>(Children.Count);
            foreach (var child in Children)
            {
                if ((child.IsDirectory && includeDirs) || (!child.IsDirectory && includeFiles))
                {
                    result.Add(child);
                }
            }
            return result;
        }

        public List<Entry> GetSongs()
        {
            var result = new List<Entry>();
            foreach (var child in Children)
            {
                if (child?.IsDirectory == false && !child.IsVideo)
                {
                    result.Add(child);
                }
            }
            return result;
        }

        [Serializable]
        public class Entry
        {
            public const int TYPE_SONG = 0;
            public const int TYPE_PODCAST = 1;
            public const int TYPE_AUDIO_BOOK = 2;

            public string Id { get; set; }
            public string Parent { get; set; }
            public string GrandParent { get; set; }
            public string AlbumId { get; set; }
            public string ArtistId { get; set; }
            public bool IsDirectory { get; set; }
            public string Title { get; set; }
            public string Album { get; set; }
            public string Artist { get; set; }
            public int Track { get; set; }
            public int CustomOrder { get; set; }
            public int Year { get; set; }
            public string Genre { get; set; }
            public string ContentType { get; set; }
            public string Suffix { get; set; }
            public string TranscodedContentType { get; set; }
            public string TranscodedSuffix { get; set; }
            public string CoverArt { get; set; }
            public long Size { get; set; }
            public int Duration { get; set; }
            public int BitRate { get; set; }
            public string Path { get; set; }
            public bool IsVideo { get; set; }
            public int DiscNumber { get;set; }
            public bool Starred { get; set; }
            public int Rating { get; set; }
            public Bookmark Bookmark { get; set; }
            public int Type { get; set; }
            public int Closeness { get; set; }
            public Artist LinkedArtist { get; set; }

            public Entry()
            {

            }

            public Entry(string id)
            {
                Id = id;
            }

            public Entry(Artist artist)
            {
                Id = artist.Id;
                Title = artist.Name;
                IsDirectory = true;
                Starred = artist.Starred;
                Rating = artist.Rating;
                LinkedArtist = artist;
            }

            public void LoadMetadata()
            {
                //TODO ?
            }

            public void RebaseTitleOffPath()
            {
                try
                {
                    var filename = Path;
                    if (filename == null)
                        return;

                    var index = filename.LastIndexOf('/');
                    if (index != -1)
                    {
                        filename = filename.Substring(index + 1);
                        filename = filename.Replace(string.Format("%02d", Track), "");

                        index = filename.LastIndexOf('.');
                        if (index != -1)
                            filename = filename.Substring(0, index);

                        Title = filename;
                    }
                }
                catch (Exception e)
                {
                    //TODO Log exception
                    // "Failed to update title based off of path"
                }
            }

            public bool IsAlbum()
            {
                return Parent != null || Artist != null;
            }

            public string GetAlbumDisplay()
            {
                if (Album != null && Title.StartsWith("Disc "))
                {
                    return Album;
                }

                return Title;
            }

            public void SetStarred(bool starred)
            {
                Starred = starred;

                if (LinkedArtist != null)
                {
                    LinkedArtist.Starred = starred;
                }
            }

            public void SetRating(int rating)
            {
                Rating = rating;

                if (LinkedArtist != null)
                    LinkedArtist.Rating = rating;
            }

            public bool IsSong()
            {
                return Type == TYPE_SONG;
            }

            public bool IsPodcast()
            {
                return this is PodcastEpisode || Type == TYPE_PODCAST;
            }

            public bool IsAudioBook()
            {
                return Type == TYPE_AUDIO_BOOK;
            }

            public override bool Equals(object o)
            {
                if (this == o)
                    return true;

                if (o == null || base.GetType() != o.GetType())
                    return false;

                var entry = (Entry) o;
                return Id.Equals(entry.Id);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public override string ToString()
            {
                return Title;
            }
        }

        public class EntryComparator : IComparer<Entry>
        {
            private readonly bool _byYear;

            public EntryComparator(bool byYear)
            {
                _byYear = byYear;
            }

            public int Compare(Entry lhs, Entry rhs)
            {
                if (lhs?.IsDirectory == true && rhs?.IsDirectory == false) return -1;
                if (lhs?.IsDirectory == false && rhs?.IsDirectory == true) return 1;
                if (lhs?.IsDirectory == true && rhs?.IsDirectory == true)
                {
                    if (_byYear)
                    {
                        return lhs.Year.CompareTo(rhs.Year);
                    }

                    return string.CompareOrdinal(lhs.GetAlbumDisplay(), rhs.GetAlbumDisplay());
                }

                var lhsDisc = lhs?.DiscNumber;
                var rhsDisc = rhs?.DiscNumber;

                if (lhsDisc != null && rhsDisc != null)
                {
                    if (lhsDisc < rhsDisc)
                        return -1;
                    if (lhsDisc > rhsDisc) return 1;
                }

                var lhsTrack = lhs?.Track;
                var rhsTrack = rhs?.Track;
                if (lhsTrack != null && rhsTrack != null && lhsTrack != rhsTrack)
                    return int.Parse(lhsTrack.ToString()).CompareTo(rhsTrack);
                if (lhsTrack != null)
                    return -1;
                if (rhsTrack != null) return 1;

                return string.CompareOrdinal(lhs.Title, rhs.Title);
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
                    //TODO Log exception
                    // "Failed to sort MusicDirectory"
                }
            }
        }
    }
}
