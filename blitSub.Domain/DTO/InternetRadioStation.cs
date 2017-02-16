namespace blitSub.Domain.DTO
{
    public class InternetRadioStation : MusicDirectory.Entry
    {
        private string homePageUrl;

        private string streamUrl;

        public string getStreamUrl()
        {
            return streamUrl;
        }

        public void setStreamUrl(string streamUrl)
        {
            this.streamUrl = streamUrl;
        }

        public string getHomePageUrl()
        {
            return homePageUrl;
        }

        public void setHomePageUrl(string homePageUrl)
        {
            this.homePageUrl = homePageUrl;
        }
    }
}