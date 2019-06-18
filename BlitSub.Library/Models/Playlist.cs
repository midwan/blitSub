using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Playlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Comment { get; set; }
        public string SongCount { get; set; }
        public bool Pub { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public int Duration { get; set; }

        public Playlist()
        {

        }

        public Playlist(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Playlist(string id, string name, string owner, string comment, string songCount, string pub,
            string created, string changed, int duration)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Comment = comment;
            SongCount = songCount;
            Pub = pub?.Equals("true") == true;
            if (created != null)
                Created = DateTime.Parse(created);
            if (changed != null)
                Changed = DateTime.Parse(changed);
            Duration = duration;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object o)
        {
            if (o == this)
            {
                return true;
            }

            if (o == null)
            {
                return false;
            }

            if (o is string)
            {
                return o.Equals(Id);
            }

            if (o.GetType() != GetType())
            {
                return false;
            }

            var playlist = (Playlist) o;
            return playlist.Id.Equals(Id);
        }

        public class PlaylistComparator : IComparer<Playlist>
        {

            public int Compare(Playlist playlist1, Playlist playlist2)
            {
                return string.Compare(playlist1?.Name, playlist2?.Name, StringComparison.Ordinal);
            }

            public static List<Playlist> Sort(List<Playlist> playlists)
            {
                playlists.Sort(new PlaylistComparator());
                return playlists;
            }
        }
    }
}
