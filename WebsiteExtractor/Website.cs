namespace WebSiteExtractor
{
    public class Website
    {
        public string Title { get; private set; }
        public string Url { get; private set; }
        public WebsiteType Type { get; private set; }

        public Website(string title, string url, WebsiteType type)
        {
            Title = title;
            Url = url;
            Type = type;
        }

        public override string ToString()
        {
            if (Title != null)
                return Title;
            return Url;
        }
    }


    public enum WebsiteType
    {
        jbzd,
        gag,
        youtube,
        kwejk,
        myepicwall,
        other,
        google
    }

    public enum BrowserType
    {
        External,
        Internal
    }
}
