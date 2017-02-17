using System;
using System.Collections.Generic;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class ArtistInfo
    {
        private string _biography;
        private string _imageUrl;
        private string _lastFmUrl;
        private List<string> _missingArtists;
        private string _musicBrainzId;
        private List<Artist> _similarArtists;

        public string GetBiography()
        {
            return _biography;
        }

        public void SetBiography(string biography)
        {
            _biography = biography;
        }

        public string GetMusicBrainzId()
        {
            return _musicBrainzId;
        }

        public void SetMusicBrainzId(string musicBrainzId)
        {
            _musicBrainzId = musicBrainzId;
        }

        public string GetLastFmUrl()
        {
            return _lastFmUrl;
        }

        public void SetLastFmUrl(string lastFMUrl)
        {
            _lastFmUrl = lastFMUrl;
        }

        public string GetImageUrl()
        {
            return _imageUrl;
        }

        public void SetImageUrl(string imageUrl)
        {
            _imageUrl = imageUrl;
        }

        public List<Artist> GetSimilarArtists()
        {
            return _similarArtists;
        }

        public void SetSimilarArtists(List<Artist> similarArtists)
        {
            _similarArtists = similarArtists;
        }

        public List<string> GetMissingArtists()
        {
            return _missingArtists;
        }

        public void SetMissingArtists(List<string> missingArtists)
        {
            _missingArtists = missingArtists;
        }
    }
}