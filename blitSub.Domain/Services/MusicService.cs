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
    public interface MusicService
    {
        void ping(Context context, ProgressListener progressListener);

        bool isLicenseValid(Context context, ProgressListener progressListener);

        List<MusicFolder> getMusicFolders(bool refresh, Context context, ProgressListener progressListener) ;

        void startRescan(Context context, ProgressListener listener);

        Indexes getIndexes(string musicFolderId, bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getMusicDirectory(string id, string name, bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getArtist(string id, string name, bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getAlbum(string id, string name, bool refresh, Context context, ProgressListener progressListener) ;

        SearchResult search(SearchCritera criteria, Context context, ProgressListener progressListener) ;

        MusicDirectory getStarredList(Context context, ProgressListener progressListener) ;

        MusicDirectory getPlaylist(bool refresh, string id, string name, Context context, ProgressListener progressListener) ;

        List<Playlist> getPlaylists(bool refresh, Context context, ProgressListener progressListener) ;

        void createPlaylist(string id, string name, List<MusicDirectory.Entry> entries, Context context, ProgressListener progressListener) ;

        void deletePlaylist(string id, Context context, ProgressListener progressListener) ;

        void addToPlaylist(string id, List<MusicDirectory.Entry> toAdd, Context context, ProgressListener progressListener) ;

        void removeFromPlaylist(string id, List<int> toRemove, Context context, ProgressListener progressListener) ;

        void overwritePlaylist(string id, string name, int toRemove, List<MusicDirectory.Entry> toAdd, Context context, ProgressListener progressListener) ;

        void updatePlaylist(string id, string name, string comment, bool pub, Context context, ProgressListener progressListener) ;

        Lyrics getLyrics(string artist, string title, Context context, ProgressListener progressListener) ;

        void scrobble(string id, bool submission, Context context, ProgressListener progressListener) ;

        MusicDirectory getAlbumList(string type, int size, int offset, bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getAlbumList(string type, string extra, int size, int offset, bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getSongList(string type, int size, int offset, Context context, ProgressListener progressListener) ;

        MusicDirectory getRandomSongs(int size, string artistId, Context context, ProgressListener progressListener) ;
        MusicDirectory getRandomSongs(int size, string folder, string genre, string startYear, string endYear, Context context, ProgressListener progressListener) ;

        string getCoverArtUrl(Context context, MusicDirectory.Entry entry) ;

        Bitmap getCoverArt(Context context, MusicDirectory.Entry entry, int size, ProgressListener progressListener, SilentBackgroundTask task) ;

        HttpURLConnection getDownloadInputStream(Context context, MusicDirectory.Entry song, long offset, int maxBitrate, SilentBackgroundTask task) ;

        string getMusicUrl(Context context, MusicDirectory.Entry song, int maxBitrate) ;

        string getVideoUrl(int maxBitrate, Context context, string id);

        string getVideoStreamUrl(string format, int Bitrate, Context context, string id) ;

        string getHlsUrl(string id, int bitRate, Context context) ;

        RemoteStatus updateJukeboxPlaylist(List<string> ids, Context context, ProgressListener progressListener) ;

        RemoteStatus skipJukebox(int index, int offsetSeconds, Context context, ProgressListener progressListener) ;

        RemoteStatus stopJukebox(Context context, ProgressListener progressListener) ;

        RemoteStatus startJukebox(Context context, ProgressListener progressListener) ;

        RemoteStatus getJukeboxStatus(Context context, ProgressListener progressListener) ;

        RemoteStatus setJukeboxGain(float gain, Context context, ProgressListener progressListener) ;

        void setStarred(List<MusicDirectory.Entry> entries, List<MusicDirectory.Entry> artists, List<MusicDirectory.Entry> albums, bool starred, ProgressListener progressListener, Context context) ;

        List<Share> getShares(Context context, ProgressListener progressListener) ;

        List<Share> createShare(List<string> ids, string description, Long expires, Context context, ProgressListener progressListener) ;

        void deleteShare(string id, Context context, ProgressListener progressListener) ;

        void updateShare(string id, string description, Long expires, Context context, ProgressListener progressListener) ;

        List<ChatMessage> getChatMessages(Long since, Context context, ProgressListener progressListener) ;

        void addChatMessage(string message, Context context, ProgressListener progressListener) ;

        List<Genre> getGenres(bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getSongsByGenre(string genre, int count, int offset, Context context, ProgressListener progressListener) ;

        MusicDirectory getTopTrackSongs(string artist, int size, Context context, ProgressListener progressListener) ;

        List<PodcastChannel> getPodcastChannels(bool refresh, Context context, ProgressListener progressListener) ;

        MusicDirectory getPodcastEpisodes(bool refresh, string id, Context context, ProgressListener progressListener) ;

        MusicDirectory getNewestPodcastEpisodes(bool refresh, Context context, ProgressListener progressListener, int count) ;

        void refreshPodcasts(Context context, ProgressListener progressListener) ;

        void createPodcastChannel(string url, Context context, ProgressListener progressListener) ;

        void deletePodcastChannel(string id, Context context, ProgressListener progressListener) ;

        void downloadPodcastEpisode(string id, Context context, ProgressListener progressListener) ;

        void deletePodcastEpisode(string id, string parent, ProgressListener progressListener, Context context) ;

        void setRating(MusicDirectory.Entry entry, int rating, Context context, ProgressListener progressListener) ;

        MusicDirectory getBookmarks(bool refresh, Context context, ProgressListener progressListener) ;

        void createBookmark(MusicDirectory.Entry entry, int position, string comment, Context context, ProgressListener progressListener) ;

        void deleteBookmark(MusicDirectory.Entry entry, Context context, ProgressListener progressListener) ;

        User getUser(bool refresh, string username, Context context, ProgressListener progressListener) ;

        List<User> getUsers(bool refresh, Context context, ProgressListener progressListener) ;

        void createUser(User user, Context context, ProgressListener progressListener) ;

        void updateUser(User user, Context context, ProgressListener progressListener) ;

        void deleteUser(string username, Context context, ProgressListener progressListener) ;

        void changeEmail(string username, string email, Context context, ProgressListener progressListener) ;

        void changePassword(string username, string password, Context context, ProgressListener progressListener) ;

        Bitmap getAvatar(string username, int size, Context context, ProgressListener progressListener, SilentBackgroundTask task) ;

        ArtistInfo getArtistInfo(string id, bool refresh, bool allowNetwork, Context context, ProgressListener progressListener) ;

        Bitmap getBitmap(string url, int size, Context context, ProgressListener progressListener, SilentBackgroundTask task) ;

        MusicDirectory getVideos(bool refresh, Context context, ProgressListener progressListener) ;

        void savePlayQueue(List<MusicDirectory.Entry> songs, MusicDirectory.Entry currentPlaying, int position, Context context, ProgressListener progressListener);

        PlayerQueue getPlayQueue(Context context, ProgressListener progressListener);

        List<InternetRadioStation> getInternetRadioStations(bool refresh, Context context, ProgressListener progressListener);

        int processOfflineSyncs(Context context, ProgressListener progressListener);

        void setInstance(int instance);
    }
}
