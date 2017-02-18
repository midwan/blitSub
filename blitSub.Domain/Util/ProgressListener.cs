namespace blitSub.Domain.Util
{
    public interface ProgressListener
    {
        void updateProgress(string message);
        void updateProgress(int messageId);
        void updateCache(int changeCode);
    }
}