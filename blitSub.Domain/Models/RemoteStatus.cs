namespace blitSub.Domain.Models
{
    public class RemoteStatus
    {
        private int currentPlayingIndex;
        private float gain;
        private bool playing;
        private int positionSeconds;

        public int getPositionSeconds()
        {
            return positionSeconds;
        }

        public void setPositionSeconds(int positionSeconds)
        {
            this.positionSeconds = positionSeconds;
        }

        public int getCurrentPlayingIndex()
        {
            return currentPlayingIndex;
        }

        public void setCurrentIndex(int currentPlayingIndex)
        {
            this.currentPlayingIndex = currentPlayingIndex;
        }

        public bool isPlaying()
        {
            return playing;
        }

        public void setPlaying(bool playing)
        {
            this.playing = playing;
        }

        public float getGain()
        {
            return gain;
        }

        public void setGain(float gain)
        {
            this.gain = gain;
        }
    }
}