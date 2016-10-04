using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WebSiteExtractor
{
    public class Extractor
    {
        private List<Website> websites;
        private Regex regex;
        private HTMLContentExtractor contentExtractor;
        private const int MaxIterations = 1000;
        private const string pattern = @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";

        public Extractor()
        {
            regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            contentExtractor = new HTMLContentExtractor();
            websites = new List<Website>();
        }

        public IReadOnlyList<Website> FindWebsiteAddresses(string text)
        {
            websites.Clear();
            var result = regex.Matches(text);
            for (int i = 0; i < result.Count; i++)
            {
                var url = result[i].Value;
                Website website = null;
                foreach (WebsiteType websiteType in Enum.GetValues(typeof(WebsiteType)))
                {
                    if (url.Contains(websiteType.ToString()))
                    {
                        website = ExtractWebsiteInfo(url, websiteType);
                        break;
                    }
                }
                if (website == null)
                    website = new Website(null, url, WebsiteType.other);
                websites.Add(website);
            }

            return new List<Website>(websites);
        }

        public string GenerateBBCode()
        {
            var stringBuilder = new StringBuilder();
            foreach (var website in websites)
            {
                if (website.Title != null)
                    stringBuilder.AppendLine("[*][url=" + website.Url + "]" + website.Title + "[/url]");
                else
                    stringBuilder.AppendLine("[*][url]" + website.Url + "[/url]");
            }

            return stringBuilder.ToString();
        }

        private Website ExtractWebsiteInfo(string url, WebsiteType websiteType)
        {
            switch (websiteType)
            {
                case WebsiteType.jbzd:
                    if(url.Contains("img.jbzd"))
                        return new Website(websiteType.ToString(), url, WebsiteType.other);
                    return new Website(ExtractRegualTitle(url), url, websiteType);
                case WebsiteType.gag:
                    return new Website(contentExtractor.Extract9GagTitle(url), url, websiteType);
                case WebsiteType.youtube:
                    return new Website(contentExtractor.ExtractYoutubeTitle(url), url, websiteType);
                case WebsiteType.kwejk:
                    if (url.Contains("i1.kwejk"))
                        return new Website(websiteType.ToString(), url, WebsiteType.other);
                    return new Website(ExtractRegualTitle(url), url, websiteType);
                case WebsiteType.mistrzowie:
                    if (url.Contains("uimages"))
                        return new Website(websiteType.ToString(), url, WebsiteType.other);
                    return new Website(ExtractRegualTitle(url), url, websiteType);
                case WebsiteType.myepicwall:
                    return new Website(websiteType.ToString(), url, websiteType);
                case WebsiteType.other:
                    return new Website(null, url, websiteType);
                default:
                    return new Website(null, url, websiteType);
            }
        }

        private string ExtractRegualTitle(string url)
        {
            var index = url.LastIndexOf("/") + 1;
            var result = url.Substring(index);
            index = result.IndexOf(".");
            return index < 0 ? result : result.Substring(0, index);
        }
    }
}
