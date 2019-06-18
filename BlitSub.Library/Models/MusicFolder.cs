using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class MusicFolder
    {
        public bool Enabled { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public MusicFolder()
        {

        }

        public MusicFolder(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public static void Sort(List<MusicFolder> musicFolders)
        {
            try
            {
                musicFolders.Sort(new MusicFolderComparator());
            }
            catch (Exception e)
            {
                //TODO Log exception
                //"Failed to sort music folders"
            }
        }

        public class MusicFolderComparator : IComparer<MusicFolder>
        {
            public int Compare(MusicFolder lhsMusicFolder, MusicFolder rhsMusicFolder)
            {
                if (lhsMusicFolder != null && (lhsMusicFolder == rhsMusicFolder || lhsMusicFolder.Name.Equals(rhsMusicFolder?.Name)))
                    return 0;
                return string.CompareOrdinal(lhsMusicFolder.Name, rhsMusicFolder.Name);
            }
        }
    }
}
