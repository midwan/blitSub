using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class Playlist
    {
        private string id;
        private string name;
        private string owner;
        private string comment;
        private string songCount;
        private bool pub;
        private DateTime created;
        private DateTime changed;
        private int duration;

        public Playlist()
        {

        }
        public Playlist(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public Playlist(string id, string name, string owner, string comment, string songCount, string pub, string created, string changed, int duration)
        {
            this.id = id;
            this.name = name;
            this.owner = owner ?? "";
            this.comment = comment ?? "";
            this.songCount = songCount ?? "";
            this.pub = pub != null && pub.Equals("true");
            setCreated(created);
            setChanged(changed);
            this.duration = duration;
        }

        public string getId()
        {
            return id;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getOwner()
        {
            return this.owner;
        }

        public void setOwner(string owner)
        {
            this.owner = owner;
        }

        public string getComment()
        {
            return this.comment;
        }

        public void setComment(string comment)
        {
            this.comment = comment;
        }

        public string getSongCount()
        {
            return this.songCount;
        }

        public void setSongCount(string songCount)
        {
            this.songCount = songCount;
        }

        public bool getPublic()
        {
            return this.pub;
        }
        public void setPublic(bool pub)
        {
            this.pub = pub;
        }

        public DateTime getCreated()
        {
            return created;
        }

        public void setCreated(string created)
        {
            if (created != null)
            {
                try
                {
                    this.created = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.ENGLISH).parse(created);
                }
                catch (Exception e)
                {
                    this.created = null;
                }
            }
            else
            {
                this.created = null;
            }
        }
        public void setCreated(DateTime created)
        {
            this.created = created;
        }

        public DateTime getChanged()
        {
            return changed;
        }
        public void setChanged(string changed)
        {
            if (changed != null)
            {
                try
                {
                    this.changed = DateTime.Parse(changed);
                }
                catch (Exception e)
                {
                    this.changed = null;
                }
            }
            else
            {
                this.changed = null;
            }
        }
        public void setChanged(DateTime changed)
        {
            this.changed = changed;
        }

        public int getDuration()
        {
            return duration;
        }
        public void setDuration(int duration)
        {
            this.duration = duration;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }

        
        public override bool Equals(object o)
        {
            if (o == this)
            {
                return true;
            }
            else if (o == null)
            {
                return false;
            }
            else if (o is string) {
                return o.Equals(this.id);
            } else if (o.GetType() != GetType())
            {
                return false;
            }

            Playlist playlist = (Playlist)o;
            return playlist.id.Equals(this.id);
        }

        public class PlaylistComparator : IComparer<Playlist> {

        public int Compare(Playlist playlist1, Playlist playlist2)
        {
            return playlist1.getName().CompareTo(playlist2.getName());
        }

        public static List<Playlist> sort(List<Playlist> playlists)
        {
                playlists.Sort(new PlaylistComparator());
            return playlists;
        }
    }
}
}
