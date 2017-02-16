using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class ArtistInfo
    {
        private string biography;
        private string imageUrl;
        private string lastFMUrl;
        private List<string> missingArtists;
        private string musicBrainzId;
        private List<Artist> similarArtists;

        public string getBiography()
        {
            return biography;
        }

        public void setBiography(string biography)
        {
            this.biography = biography;
        }

        public string getMusicBrainzId()
        {
            return musicBrainzId;
        }

        public void setMusicBrainzId(string musicBrainzId)
        {
            this.musicBrainzId = musicBrainzId;
        }

        public string getLastFMUrl()
        {
            return lastFMUrl;
        }

        public void setLastFMUrl(string lastFMUrl)
        {
            this.lastFMUrl = lastFMUrl;
        }

        public string getImageUrl()
        {
            return imageUrl;
        }

        public void setImageUrl(string imageUrl)
        {
            this.imageUrl = imageUrl;
        }

        public List<Artist> getSimilarArtists()
        {
            return similarArtists;
        }

        public void setSimilarArtists(List<Artist> similarArtists)
        {
            this.similarArtists = similarArtists;
        }

        public List<string> getMissingArtists()
        {
            return missingArtists;
        }

        public void setMissingArtists(List<string> missingArtists)
        {
            this.missingArtists = missingArtists;
        }
    }
}