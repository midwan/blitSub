using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class User
    {
        public const string SCROBBLING = "scrobblingEnabled";
        public const string ADMIN = "adminRole";
        public const string SETTINGS = "settingsRole";
        public const string DOWNLOAD = "downloadRole";
        public const string UPLOAD = "uploadRole";
        public const string COVERART = "coverArtRole";
        public const string COMMENT = "commentRole";
        public const string PODCAST = "podcastRole";
        public const string STREAM = "streamRole";
        public const string JUKEBOX = "jukeboxRole";
        public const string SHARE = "shareRole";
        public const string VIDEO_CONVERSION = "videoConversionRole";
        public const string LASTFM = "lastFMRole";
        public static readonly List<string> ROLES = new List<string>();
        
        private string _email;
        private List<Setting> musicFolders;
        private string _password;

        private readonly List<Setting> settings = new List<Setting>();

        private string username;

        public User()
        {
            ROLES.Add(ADMIN);
            ROLES.Add(SETTINGS);
            ROLES.Add(STREAM);
            ROLES.Add(DOWNLOAD);
            ROLES.Add(UPLOAD);
            ROLES.Add(COVERART);
            ROLES.Add(COMMENT);
            ROLES.Add(PODCAST);
            ROLES.Add(JUKEBOX);
            ROLES.Add(SHARE);
            ROLES.Add(VIDEO_CONVERSION);
        }

        [Serializable]
        public class Setting
        {
            private string _name;
            private bool _value;

            public Setting()
            {
            }

            public Setting(string name, bool value)
            {
                _name = name;
                _value = value;
            }

            public string GetName()
            {
                return _name;
            }

            public void SetName(string name)
            {
                _name = name;
            }

            public bool GetValue()
            {
                return _value;
            }

            public void SetValue(bool value)
            {
                _value = value;
            }
        }

        public class MusicFolderSetting : Setting
        {
            private readonly string _label;

            public MusicFolderSetting()
            {
            }

            public MusicFolderSetting(string name, string label, bool value)
            {
                SetName(name);
                SetValue(value);
                _label = label;
            }

            public string GetLabel()
            {
                return _label;
            }
        }
    }
}
