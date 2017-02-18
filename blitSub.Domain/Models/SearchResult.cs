using System;
using System.Collections.Generic;
using System.Linq;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class SearchResult
    {
        private readonly List<MusicDirectory.Entry> albums;
        private readonly List<Artist> artists;
        private readonly List<MusicDirectory.Entry> songs;

        public SearchResult(List<Artist> artists, List<MusicDirectory.Entry> albums, List<MusicDirectory.Entry> songs)
        {
            this.artists = artists;
            this.albums = albums;
            this.songs = songs;
        }

        public List<Artist> getArtists()
        {
            return artists;
        }

        public List<MusicDirectory.Entry> getAlbums()
        {
            return albums;
        }

        public List<MusicDirectory.Entry> getSongs()
        {
            return songs;
        }

        public bool hasArtists()
        {
            return artists.Any();
        }

        public bool hasAlbums()
        {
            return albums.Any();
        }

        public bool hasSongs()
        {
            return songs.Any();
        }
    }
}