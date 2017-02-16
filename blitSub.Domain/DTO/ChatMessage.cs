using System;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class ChatMessage
    {
        private string message;
        private long time;

        private string username;

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public long getTime()
        {
            return time;
        }

        public void setTime(long time)
        {
            this.time = time;
        }

        public string getMessage()
        {
            return message;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }
    }
}