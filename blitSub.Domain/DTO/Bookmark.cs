using System;
using System.Globalization;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Bookmark
    {
        private DateTime changed;
        private string comment;
        private DateTime created;

        private int position;
        private string username;

        public Bookmark()
        {
        }

        public Bookmark(int position)
        {
            this.position = position;
        }

        public int getPosition()
        {
            return position;
        }

        public void setPosition(int position)
        {
            this.position = position;
        }

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getComment()
        {
            return comment;
        }

        public void setComment(string comment)
        {
            this.comment = comment;
        }

        public DateTime getCreated()
        {
            return created;
        }

        public void setCreated(string created)
        {
            if (created != null)
                try
                {
                    this.created = DateTime.ParseExact(created, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    this.created = DateTime.MinValue;
                }
            else
                this.created = DateTime.MinValue;
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
                try
                {
                    this.changed = DateTime.ParseExact(changed, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    this.changed = DateTime.MinValue;
                }
            else
                this.changed = DateTime.MinValue;
        }

        public void setChanged(DateTime changed)
        {
            this.changed = changed;
        }
    }
}