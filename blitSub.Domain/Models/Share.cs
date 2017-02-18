using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class Share
    {
        private string id;
        private string url;
        private string description;
        private string username;
        private DateTime? created;
        private DateTime? lastVisited;
        private DateTime? expires;
        private long visitCount;
        private List<MusicDirectory.Entry> entries;

        public Share()
        {
            entries = new List<MusicDirectory.Entry>();
        }

        public string getName()
        {
            if (description != null && !"".Equals(description))
            {
                return description;
            }
            else
            {
                return url.Replace(".*/([^/?]+).*", "$1");
            }
        }

        public string getId()
        {
            return id;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public string getUrl()
        {
            return url;
        }

        public void setUrl(string url)
        {
            this.url = url;
        }

        public string getDescription()
        {
            return description;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public DateTime? getCreated()
        {
            return created;
        }

        public void setCreated(string created)
        {
            if (created != null)
            {
                try
                {
                    this.created = DateTime.Parse(created);
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

        public DateTime? getLastVisited()
        {
            return lastVisited;
        }

        public void setLastVisited(string lastVisited)
        {
            if (lastVisited != null)
            {
                try
                {
                    this.lastVisited = DateTime.Parse(lastVisited);
                }
                catch (Exception e)
                {
                    this.lastVisited = null;
                }
            }
            else
            {
                this.lastVisited = null;
            }
        }
        public void setLastVisited(DateTime lastVisited)
        {
            this.lastVisited = lastVisited;
        }

        public DateTime? getExpires()
        {
            return expires;
        }

        public void setExpires(string expires)
        {
            if (expires != null)
            {
                try
                {
                    this.expires = DateTime.Parse(expires);
                }
                catch (Exception e)
                {
                    this.expires = null;
                }
            }
            else
            {
                this.expires = null;
            }
        }
        public void setExpires(DateTime expires)
        {
            this.expires = expires;
        }

        public long getVisitCount()
        {
            return visitCount;
        }

        public void setVisitCount(long visitCount)
        {
            this.visitCount = visitCount;
        }

        public MusicDirectory getMusicDirectory()
        {
            MusicDirectory dir = new MusicDirectory();
            dir.AddChildren(entries);
            dir.SetId(getId());
            dir.SetName(getName());
            return dir;
        }

        public List<MusicDirectory.Entry> getEntries()
        {
            return this.entries;
        }

        public void addEntry(MusicDirectory.Entry entry)
        {
            entries.Add(entry);
        }
    }
}
