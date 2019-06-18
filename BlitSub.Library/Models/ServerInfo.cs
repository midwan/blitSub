using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class ServerInfo
    {
        public const int TYPE_SUBSONIC = 1;
        public const int TYPE_MADSONIC = 2;
        public const int TYPE_AMPACHE = 3;

        public bool IsLicenseValid { get; set; }
        public Version RestVersion { get;set; }
        public int Type { get; set; }

        public ServerInfo()
        {
            Type = TYPE_SUBSONIC;
        }

        public bool IsStockSubsonic()
        {
            return Type == TYPE_SUBSONIC;
        }

        public bool IsMadsonic()
        {
            return Type == TYPE_MADSONIC;
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }

            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            var info = (ServerInfo)o;

            if (Type != info.Type)
            {
                return false;
            }

            if (RestVersion == null || info.RestVersion == null)
            {
                // Should never be null unless just starting up
                return false;
            }

            return RestVersion.Equals(info.RestVersion);
        }


    }
}
