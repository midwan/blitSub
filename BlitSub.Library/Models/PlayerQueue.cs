using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class PlayerQueue
    {
        public DateTime? Changed = null;
        public int CurrentPlayingIndex;
        public int CurrentPlayingPosition;
        public bool RenameCurrent = false;
        public List<MusicDirectory.Entry> Songs = new List<MusicDirectory.Entry>();
        public List<MusicDirectory.Entry> ToDelete = new List<MusicDirectory.Entry>();
    }
}
