using System;

namespace blitSub.Domain
{
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
                    this.created = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.ENGLISH).parse(created);
                }
                catch (ParseException e)
                {
                    this.created = null;
                }
            else
                this.created = null;
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
                    this.changed = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.ENGLISH).parse(changed);
                }
                catch (ParseException e)
                {
                    this.changed = null;
                }
            else
                this.changed = null;
        }

        public void setChanged(DateTime changed)
        {
            this.changed = changed;
        }
    }
}