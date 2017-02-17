using System;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class Lyrics
    {
        private string _artist;
        private string _text;
        private string _title;

        public string GetArtist()
        {
            return _artist;
        }

        public void SetArtist(string artist)
        {
            _artist = artist;
        }

        public string GetTitle()
        {
            return _title;
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        public string GetText()
        {
            return _text;
        }

        public void SetText(string text)
        {
            _text = text;
        }
    }
}