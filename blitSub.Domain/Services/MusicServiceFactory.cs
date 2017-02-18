using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Services
{
    public class MusicServiceFactory
    {
        private static readonly MusicService REST_MUSIC_SERVICE = new CachedMusicService(new RESTMusicService());
        private static readonly MusicService OFFLINE_MUSIC_SERVICE = new OfflineMusicService();

        public static MusicService getMusicService(Context context)
        {
            return Util.isOffline(context) ? OFFLINE_MUSIC_SERVICE : REST_MUSIC_SERVICE;
        }
    }
}
