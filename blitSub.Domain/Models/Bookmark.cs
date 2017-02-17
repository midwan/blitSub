using System;
using System.Globalization;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Bookmark
    {
        private DateTime _changed;
        private string _comment;
        private DateTime _created;

        private int _position;
        private string _username;

        public Bookmark()
        {
        }

        public Bookmark(int position)
        {
            _position = position;
        }

        public int GetPosition()
        {
            return _position;
        }

        public void SetPosition(int position)
        {
            _position = position;
        }

        public string GetUsername()
        {
            return _username;
        }

        public void SetUsername(string username)
        {
            _username = username;
        }

        public string GetComment()
        {
            return _comment;
        }

        public void SetComment(string comment)
        {
            _comment = comment;
        }

        public DateTime GetCreated()
        {
            return _created;
        }

        public void SetCreated(string created)
        {
            if (created != null)
                try
                {
                    _created = DateTime.ParseExact(created, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    _created = DateTime.MinValue;
                }
            else
                _created = DateTime.MinValue;
        }

        public void SetCreated(DateTime created)
        {
            _created = created;
        }

        public DateTime GetChanged()
        {
            return _changed;
        }

        public void SetChanged(string changed)
        {
            if (changed != null)
                try
                {
                    _changed = DateTime.ParseExact(changed, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    _changed = DateTime.MinValue;
                }
            else
                _changed = DateTime.MinValue;
        }

        public void SetChanged(DateTime changed)
        {
            _changed = changed;
        }
    }
}