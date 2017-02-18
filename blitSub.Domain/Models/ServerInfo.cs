using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class ServerInfo
    {
        public static readonly int TYPE_SUBSONIC = 1;
        public static readonly int TYPE_MADSONIC = 2;
        public static readonly int TYPE_AMPACHE = 3;
        private static readonly Map<Integer, ServerInfo> SERVERS = new ConcurrentHashMap<Integer, ServerInfo>();

        private bool isLicenseValid;
        private Version restVersion;
        private int type;

        public ServerInfo()
        {
            type = TYPE_SUBSONIC;
        }

        public bool isLicenseValid()
        {
            return isLicenseValid;
        }

        public void setLicenseValid(bool licenseValid)
        {
            isLicenseValid = licenseValid;
        }

        public Version getRestVersion()
        {
            return restVersion;
        }

        public void setRestVersion(Version restVersion)
        {
            this.restVersion = restVersion;
        }

        public int getRestType()
        {
            return type;
        }
        public void setRestType(int type)
        {
            this.type = type;
        }

        public bool isStockSubsonic()
        {
            return type == TYPE_SUBSONIC;
        }
        public bool isMadsonic()
        {
            return type == TYPE_MADSONIC;
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            else if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            var info = (ServerInfo)o;

            if (this.type != info.type)
            {
                return false;
            }
            else if (this.restVersion == null || info.restVersion == null)
            {
                // Should never be null unless just starting up
                return false;
            }
            else
            {
                return this.restVersion.Equals(info.restVersion);
            }
        }

        // Stub to make sure this is never used, too easy to screw up
        private void saveServerInfo(Context context)
        {

        }
        public void saveServerInfo(Context context, int instance)
        {
            ServerInfo current = SERVERS.get(instance);
            if (!this.Equals(current))
            {
                SERVERS.put(instance, this);
                FileUtil.serialize(context, this, getCacheName(context, instance));
            }
        }

        public static ServerInfo getServerInfo(Context context)
        {
            return getServerInfo(context, Util.getActiveServer(context));
        }
        public static ServerInfo getServerInfo(Context context, int instance)
        {
            ServerInfo current = SERVERS.get(instance);
            if (current != null)
            {
                return current;
            }

            current = FileUtil.deserialize(context, getCacheName(context, instance), ServerInfo.class);
		if(current != null) {
			SERVERS.put(instance, current);
		}
		
		return current;
	}

public static Version getServerVersion(Context context)
{
    return getServerVersion(context, Util.getActiveServer(context));
}
public static Version getServerVersion(Context context, int instance)
{
    ServerInfo server = getServerInfo(context, instance);
    if (server == null)
    {
        return null;
    }

    return server.getRestVersion();
}

public static bool checkServerVersion(Context context, string requiredVersion)
{
    return checkServerVersion(context, requiredVersion, Util.getActiveServer(context));
}
public static bool checkServerVersion(Context context, string requiredVersion, int instance)
{
    ServerInfo server = getServerInfo(context, instance);
    if (server == null)
    {
        return false;
    }

    Version version = server.getRestVersion();
    if (version == null)
    {
        return false;
    }

    Version required = new Version(requiredVersion);
    return version.CompareTo(required) >= 0;
}

public static int getServerType(Context context)
{
    return getServerType(context, Util.getActiveServer(context));
}
public static int getServerType(Context context, int instance)
{
    if (Util.isOffline(context))
    {
        return 0;
    }

    ServerInfo server = getServerInfo(context, instance);
    if (server == null)
    {
        return 0;
    }

    return server.getRestType();
}

public static bool isStockSubsonic(Context context)
{
    return isStockSubsonic(context, Util.getActiveServer(context));
}
public static bool isStockSubsonic(Context context, int instance)
{
    return getServerType(context, instance) == TYPE_SUBSONIC;
}

public static bool isMadsonic(Context context)
{
    return isMadsonic(context, Util.getActiveServer(context));
}
public static bool isMadsonic(Context context, int instance)
{
    return getServerType(context, instance) == TYPE_MADSONIC;
}
public static bool isMadsonic6(Context context)
{
    return isMadsonic6(context, Util.getActiveServer(context));
}
public static bool isMadsonic6(Context context, int instance)
{
    return getServerType(context, instance) == TYPE_MADSONIC && checkServerVersion(context, "2.0", instance);
}

public static bool isAmpache(Context context)
{
    return isAmpache(context, Util.getActiveServer(context));
}
public static bool isAmpache(Context context, int instance)
{
    return getServerType(context, instance) == TYPE_AMPACHE;
}

private static string getCacheName(Context context, int instance)
{
    return "server-" + Util.getRestUrl(context, null, instance, false).hashCode() + ".ser";
}

public static bool hasArtistInfo(Context context)
{
    if (!isMadsonic(context) && ServerInfo.checkServerVersion(context, "1.11"))
    {
        return true;
    }
    else if (isMadsonic(context))
    {
        return checkServerVersion(context, "2.0");
    }
    else
    {
        return false;
    }
}

public static bool canBookmark(Context context)
{
    return checkServerVersion(context, "1.9");
}
public static bool canInternetRadio(Context context)
{
    return checkServerVersion(context, "1.9");
}

public static bool canSavePlayQueue(Context context)
{
    return ServerInfo.checkServerVersion(context, "1.12") && (!ServerInfo.isMadsonic(context) || checkServerVersion(context, "2.0"));
}

public static bool canAlbumListPerFolder(Context context)
{
    return ServerInfo.checkServerVersion(context, "1.11") && (!ServerInfo.isMadsonic(context) || checkServerVersion(context, "2.0")) && !Util.isTagBrowsing(context);
}
public static bool hasTopSongs(Context context)
{
    return ServerInfo.isMadsonic(context) || ServerInfo.checkServerVersion(context, "1.13");
}

public static bool canUseToken(Context context)
{
    return canUseToken(context, Util.getActiveServer(context));
}
public static bool canUseToken(Context context, int instance)
{
    if (isStockSubsonic(context, instance) && checkServerVersion(context, "1.14", instance))
    {
        if (Util.getBlockTokenUse(context, instance))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    else
    {
        return false;
    }
}
public static bool hasSimilarArtists(Context context)
{
    return !ServerInfo.isMadsonic(context) || ServerInfo.checkServerVersion(context, "2.0");
}
public static bool hasNewestPodcastEpisodes(Context context)
{
    return ServerInfo.checkServerVersion(context, "1.13");
}
    }
}
