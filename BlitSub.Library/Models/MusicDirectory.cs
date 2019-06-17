using System;
using System.Collections.Generic;
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


        }
    }
}
