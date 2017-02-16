using System;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Lyrics
    {
        private string artist;
        private string text;
        private string title;

        public string getArtist()
        {
            return artist;
        }

        public void setArtist(string artist)
        {
            this.artist = artist;
        }

        public string getTitle()
        {
            return title;
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public string getText()
        {
            return text;
        }

        public void setText(string text)
        {
            this.text = text;
        }
    }
}