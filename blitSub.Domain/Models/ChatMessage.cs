using System;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class ChatMessage
    {
        private string _message;
        private long _time;

        private string _username;

        public string GetUsername()
        {
            return _username;
        }

        public void SetUsername(string username)
        {
            _username = username;
        }

        public long GetTime()
        {
            return _time;
        }

        public void SetTime(long time)
        {
            _time = time;
        }

        public string GetMessage()
        {
            return _message;
        }

        public void SetMessage(string message)
        {
            _message = message;
        }
    }
}