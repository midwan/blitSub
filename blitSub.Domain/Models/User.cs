using System;
using System.Collections.Generic;
using blitSub.Domain.DTO;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class User
    {
        public static readonly string SCROBBLING = "scrobblingEnabled";
        public static readonly string ADMIN = "adminRole";
        public static readonly string SETTINGS = "settingsRole";
        public static readonly string DOWNLOAD = "downloadRole";
        public static readonly string UPLOAD = "uploadRole";
        public static readonly string COVERART = "coverArtRole";
        public static readonly string COMMENT = "commentRole";
        public static readonly string PODCAST = "podcastRole";
        public static readonly string STREAM = "streamRole";
        public static readonly string JUKEBOX = "jukeboxRole";
        public static readonly string SHARE = "shareRole";
        public static readonly string VIDEO_CONVERSION = "videoConversionRole";
        public static readonly string LASTFM = "lastFMRole";
        public static readonly List<string> ROLES = new List<string>();
        private string email;
        private List<Setting> musicFolders;
        private string password;

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

        public string getUsername()
        {
            return username;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getPassword()
        {
            return password;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public List<Setting> getSettings()
        {
            return settings;
        }

        public void setSettings(List<Setting> settings)
        {
            this.settings.Clear();
            this.settings.AddRange(settings);
        }

        public void addSetting(string name, bool value)
        {
            settings.Add(new Setting(name, value));
        }

        public void addMusicFolder(MusicFolder musicFolder)
        {
            if (musicFolders == null)
                musicFolders = new List<Setting>();

            musicFolders.Add(new MusicFolderSetting(musicFolder.GetId(), musicFolder.GetName(), false));
        }

        public void addMusicFolder(MusicFolderSetting musicFolderSetting, bool defaultValue)
        {
            if (musicFolders == null)
                musicFolders = new List<Setting>();

            musicFolders.Add(new MusicFolderSetting(musicFolderSetting.getName(), musicFolderSetting.getLabel(),
                defaultValue));
        }

        public List<Setting> getMusicFolderSettings()
        {
            return musicFolders;
        }

        [Serializable]
        public class Setting
        {
            private readonly string name;
            private bool value;

            public Setting()
            {
            }

            public Setting(string name, bool value)
            {
                this.name = name;
                this.value = value;
            }

            public string getName()
            {
                return name;
            }

            public bool getValue()
            {
                return value;
            }

            public void setValue(bool value)
            {
                this.value = value;
            }
        }

        public class MusicFolderSetting : Setting
        {
            private readonly string label;

            public MusicFolderSetting()
            {
            }

            public MusicFolderSetting(string name, string label, bool value)
            {
                super(name, value);
                this.label = label;
            }

            public string getLabel()
            {
                return label;
            }
        }
    }
}