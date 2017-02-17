namespace blitSub.Domain.Models
{
    public class InternetRadioStation : MusicDirectory.Entry
    {
        private string _homePageUrl;

        private string _streamUrl;

        public string GetStreamUrl()
        {
            return _streamUrl;
        }

        public void SetStreamUrl(string streamUrl)
        {
            _streamUrl = streamUrl;
        }

        public string GetHomePageUrl()
        {
            return _homePageUrl;
        }

        public void SetHomePageUrl(string homePageUrl)
        {
            _homePageUrl = homePageUrl;
        }
    }
}