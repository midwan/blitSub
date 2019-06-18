using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    public class RemoteStatus
    {
        public int CurrentPlayingIndex { get; set; }
        public float Gain { get; set; }
        public bool Playing { get; set; }
        public int PositionSeconds { get; set; }

    }
}
