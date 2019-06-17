using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Lyrics
    {
        public string Artist { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
