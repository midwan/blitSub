using System;
using System.Collections.Generic;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class SearchResult
    {
        private readonly List<MusicDirectory.Entry> _albums;
        private readonly List<Artist> _artists;
        private readonly List<MusicDirectory.Entry> _songs;

        public SearchResult(List<Artist> artists, List<MusicDirectory.Entry> albums, List<MusicDirectory.Entry> songs)
        {
            _artists = artists;
            _albums = albums;
            _songs = songs;
        }

        public List<Artist> GetArtists()
        {
            return _artists;
        }

        public List<MusicDirectory.Entry> GetAlbums()
        {
            return _albums;
        }

        public List<MusicDirectory.Entry> GetSongs()
        {
            return _songs;
        }

        public bool HasArtists()
        {
            return _artists.Count > 0;
        }

        public bool HasAlbums()
        {
            return _albums.Count > 0;
        }

        public bool HasSongs()
        {
            return _songs.Count > 0;
        }
    }
}