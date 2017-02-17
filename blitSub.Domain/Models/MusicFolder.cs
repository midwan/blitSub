using System;
using System.Collections.Generic;

namespace blitSub.Domain.DTO
{
    [Serializable]
    public class MusicFolder
    {
        private readonly string TAG;
        private bool _enabled;
        private readonly string _id;
        private readonly string _name;

        public MusicFolder()
        {
            TAG = GetName();
        }

        public MusicFolder(string id, string name)
        {
            _id = id;
            TAG = GetName();
            _name = name;
        }

        public string GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public bool GetEnabled()
        {
            return _enabled;
        }

        public static void Sort(List<MusicFolder> musicFolders)
        {
            try
            {
                musicFolders.Sort(new MusicFolderComparator());
            }
            catch (Exception e)
            {
                Log.w(TAG, "Failed to sort music folders", e);
            }
        }

        public class MusicFolderComparator : IComparer<MusicFolder>
        {
            public int Compare(MusicFolder lhsMusicFolder, MusicFolder rhsMusicFolder)
            {
                if (lhsMusicFolder == rhsMusicFolder || lhsMusicFolder.GetName().Equals(rhsMusicFolder.GetName()))
                    return 0;
                return string.Compare(lhsMusicFolder.GetName(), rhsMusicFolder.GetName(), StringComparison.Ordinal);
            }
        }
    }
}