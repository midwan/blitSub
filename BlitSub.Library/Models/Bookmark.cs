using System;
using System.Globalization;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Bookmark
    {
        public Bookmark()
        {
        }

        public Bookmark(int position)
        {
            Position = position;
        }

        public DateTime Changed { get; private set; }
        public string Comment { get; set; }
        public DateTime Created { get; private set; }
        public int Position { get; set; }
        public string Username { get; set; }

        public void SetChanged(string changed)
        {
            if (changed != null)
                try
                {
                    Changed = DateTime.ParseExact(changed, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    Changed = DateTime.MinValue;
                }
            else
                Changed = DateTime.MinValue;
        }

        public void SetCreated(string created)
        {
            if (created != null)
                try
                {
                    Created = DateTime.ParseExact(created, "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    Created = DateTime.MinValue;
                }
            else
                Created = DateTime.MinValue;
        }
    }
}