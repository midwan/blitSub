using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blitSub.Domain.DTO;
using blitSub.Domain.Models;
using blitSub.Domain.Util;

namespace blitSub.Domain.Services
{
    public class CachedMusicService : MusicService
    {
        private static readonly string TAG = CachedMusicService.getSimpleName();

        private static readonly int MUSIC_DIR_CACHE_SIZE = 20;
        private static readonly int TTL_MUSIC_DIR = 5 * 60; // Five minutes
        public static readonly int CACHE_UPDATE_LIST = 1;
        public static readonly int CACHE_UPDATE_METADATA = 2;
        private static readonly int CACHED_LAST_FM = 24 * 60;

        private readonly RESTMusicService musicService;
    private readonly TimeLimitedCache<bool> cachedLicenseValid = new TimeLimitedCache<bool>(120, TimeUnit.SECONDS);
        private readonly TimeLimitedCache<Indexes> cachedIndexes = new TimeLimitedCache<Indexes>(60 * 60, TimeUnit.SECONDS);
        private readonly TimeLimitedCache<List<Playlist>> cachedPlaylists = new TimeLimitedCache<List<Playlist>>(3600, TimeUnit.SECONDS);
        private readonly TimeLimitedCache<List<MusicFolder>> cachedMusicFolders = new TimeLimitedCache<List<MusicFolder>>(10 * 3600, TimeUnit.SECONDS);
        private readonly TimeLimitedCache<List<PodcastChannel>> cachedPodcastChannels = new TimeLimitedCache<List<PodcastChannel>>(10 * 3600, TimeUnit.SECONDS);
        private string restUrl;
        private string musicFolderId;
        private bool isTagBrowsing = false;

        public CachedMusicService(RESTMusicService musicService)
        {
            this.musicService = musicService;
        }

        public void ping(Context context, ProgressListener progressListener)
        {
            checkSettingsChanged(context);
            musicService.ping(context, progressListener);
        }

        public bool isLicenseValid(Context context, ProgressListener progressListener)
        {
            checkSettingsChanged(context);
            var result = cachedLicenseValid.get();
            if (result == null)
            {
                result = FileUtil.deserialize(context, getCacheName(context, "license"), Boolean.class);

			if(result == null) {
            	result = musicService.isLicenseValid(context, progressListener);

				// Only save a copy license is valid
				if(result) {
					FileUtil.serialize(context, (Boolean) result, getCacheName(context, "license"));
				    }
                }
                cachedLicenseValid.set(result, result? 30L * 60L : 2L * 60L, TimeUnit.SECONDS);
            }
            return result;
        }

        public List<MusicFolder> getMusicFolders(bool refresh, Context context, ProgressListener progressListener)
        {
            checkSettingsChanged(context);
            if (refresh)
            {
                cachedMusicFolders.clear();
            }
            List<MusicFolder> result = cachedMusicFolders.get();
            if (result == null)
            {
                if (!refresh)
                {
                    result = FileUtil.deserialize(context, getCacheName(context, "musicFolders"), ArrayList.class);
        	        }

        	        if(result == null) {
            	        result = musicService.getMusicFolders(refresh, context, progressListener);
            	        FileUtil.serialize(context, new List<MusicFolder>(result), getCacheName(context, "musicFolders"));
        	        }

			        MusicFolder.Sort(result);
                    cachedMusicFolders.set(result);
                }
            return result;
        }

        public void startRescan(Context context, ProgressListener listener)
        {
            musicService.startRescan(context, listener);
}

        public Indexes getIndexes(string musicFolderId, bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getMusicDirectory(string id, string name, bool refresh, Context context,
            ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getArtist(string id, string name, bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getAlbum(string id, string name, bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public SearchResult search(SearchCritera criteria, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getStarredList(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getPlaylist(bool refresh, string id, string name, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<Playlist> getPlaylists(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void createPlaylist(string id, string name, List<MusicDirectory.Entry> entries, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void deletePlaylist(string id, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void addToPlaylist(string id, List<MusicDirectory.Entry> toAdd, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void removeFromPlaylist(string id, List<int> toRemove, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void overwritePlaylist(string id, string name, int toRemove, List<MusicDirectory.Entry> toAdd, Context context,
            ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void updatePlaylist(string id, string name, string comment, bool pub, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public Lyrics getLyrics(string artist, string title, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void scrobble(string id, bool submission, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getAlbumList(string type, int size, int offset, bool refresh, Context context,
            ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getAlbumList(string type, string extra, int size, int offset, bool refresh, Context context,
            ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getSongList(string type, int size, int offset, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getRandomSongs(int size, string artistId, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getRandomSongs(int size, string folder, string genre, string startYear, string endYear, Context context,
            ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public string getCoverArtUrl(Context context, MusicDirectory.Entry entry)
        {
            throw new NotImplementedException();
        }

        public Bitmap getCoverArt(Context context, MusicDirectory.Entry entry, int size, ProgressListener progressListener, SilentBackgroundTask task)
        {
            throw new NotImplementedException();
        }

        public HttpURLConnection getDownloadInputStream(Context context, MusicDirectory.Entry song, long offset, int maxBitrate,
            SilentBackgroundTask task)
        {
            throw new NotImplementedException();
        }

        public string getMusicUrl(Context context, MusicDirectory.Entry song, int maxBitrate)
        {
            throw new NotImplementedException();
        }

        public string getVideoUrl(int maxBitrate, Context context, string id)
        {
            throw new NotImplementedException();
        }

        public string getVideoStreamUrl(string format, int Bitrate, Context context, string id)
        {
            throw new NotImplementedException();
        }

        public string getHlsUrl(string id, int bitRate, Context context)
        {
            throw new NotImplementedException();
        }

        public RemoteStatus updateJukeboxPlaylist(List<string> ids, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public RemoteStatus skipJukebox(int index, int offsetSeconds, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public RemoteStatus stopJukebox(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public RemoteStatus startJukebox(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public RemoteStatus getJukeboxStatus(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public RemoteStatus setJukeboxGain(float gain, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void setStarred(List<MusicDirectory.Entry> entries, List<MusicDirectory.Entry> artists, List<MusicDirectory.Entry> albums, bool starred, ProgressListener progressListener,
            Context context)
        {
            throw new NotImplementedException();
        }

        public List<Share> getShares(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<Share> createShare(List<string> ids, string description, Long expires, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void deleteShare(string id, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void updateShare(string id, string description, Long expires, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<ChatMessage> getChatMessages(Long since, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void addChatMessage(string message, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<Genre> getGenres(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getSongsByGenre(string genre, int count, int offset, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getTopTrackSongs(string artist, int size, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<PodcastChannel> getPodcastChannels(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getPodcastEpisodes(bool refresh, string id, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getNewestPodcastEpisodes(bool refresh, Context context, ProgressListener progressListener, int count)
        {
            throw new NotImplementedException();
        }

        public void refreshPodcasts(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void createPodcastChannel(string url, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void deletePodcastChannel(string id, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void downloadPodcastEpisode(string id, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void deletePodcastEpisode(string id, string parent, ProgressListener progressListener, Context context)
        {
            throw new NotImplementedException();
        }

        public void setRating(MusicDirectory.Entry entry, int rating, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getBookmarks(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void createBookmark(MusicDirectory.Entry entry, int position, string comment, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void deleteBookmark(MusicDirectory.Entry entry, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public User getUser(bool refresh, string username, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<User> getUsers(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void createUser(User user, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void updateUser(User user, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(string username, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void changeEmail(string username, string email, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void changePassword(string username, string password, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public Bitmap getAvatar(string username, int size, Context context, ProgressListener progressListener,
            SilentBackgroundTask task)
        {
            throw new NotImplementedException();
        }

        public ArtistInfo getArtistInfo(string id, bool refresh, bool allowNetwork, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public Bitmap getBitmap(string url, int size, Context context, ProgressListener progressListener, SilentBackgroundTask task)
        {
            throw new NotImplementedException();
        }

        public MusicDirectory getVideos(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void savePlayQueue(List<MusicDirectory.Entry> songs, MusicDirectory.Entry currentPlaying, int position, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public PlayerQueue getPlayQueue(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public List<InternetRadioStation> getInternetRadioStations(bool refresh, Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public int processOfflineSyncs(Context context, ProgressListener progressListener)
        {
            throw new NotImplementedException();
        }

        public void setInstance(int instance)
        {
            throw new NotImplementedException();
        }
    }
}
