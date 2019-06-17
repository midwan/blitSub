using System.Collections.Generic;

namespace BlitSub.Library.Models
{
    public class ArtistInfo
    {
        public string Biography { get; set; }
        public string ImageUrl { get; set; }
        public string LastFmUrl { get; set; }
        public string MusicBrainzId { get; set; }
        public List<string> SimilarArtists { get; set; }
        public List<string> MissingArtists { get; set; }
    }
}