using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Share
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastVisited { get; set; }
        public DateTime? Expires { get; set; }
        public long VisitCount { get; set; }
        public List<MusicDirectory.Entry> Entries { get; set; }

        public Share()
        {
            Entries = new List<MusicDirectory.Entry>();
        }

        public string GetName()
        {
            if (Description != null && !"".Equals(Description))
            {
                return Description;
            }

            return Url.Replace(".*/([^/?]+).*", "$1");
        }

        public void SetCreated(string created)
        {
            if (created != null)
            {
                try
                {
                    Created = DateTime.Parse(created);
                }
                catch (Exception)
                {
                    Created = null;
                }
            }
            else
            {
                Created = null;
            }
        }

        public void SetLastVisited(string lastVisited)
        {
            if (lastVisited != null)
            {
                try
                {
                    LastVisited = DateTime.Parse(lastVisited);
                }
                catch (Exception)
                {
                    LastVisited = null;
                }
            }
            else
            {
                LastVisited = null;
            }
        }

        public void SetExpires(string expires)
        {
            if (expires != null)
            {
                try
                {
                    Expires = DateTime.Parse(expires);
                }
                catch (Exception)
                {
                    Expires = null;
                }
            }
            else
            {
                Expires = null;
            }
        }

        public MusicDirectory GetMusicDirectory()
        {
            var dir = new MusicDirectory();
            dir.Children.AddRange(Entries);
            dir.Id = Id;
            dir.Name = GetName();
            return dir;
        }
    }
}
