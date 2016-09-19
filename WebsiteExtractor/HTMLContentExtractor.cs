using System;
using System.Text.RegularExpressions;

namespace WebSiteExtractor
{
    public class HTMLContentExtractor
    {
        private System.Net.WebClient webClient;
        private string[] charsToRemove;


        public HTMLContentExtractor()
        {
            webClient = new System.Net.WebClient();
            charsToRemove = new string[] { "@", ",", ".", ";", "'" };
        }

        public string Extract9GagTitle(string url)
        {
            var checkTag = "<div id=\"container\"";
            var beginTitleTag = "<h2 class=\"badge-item-title\">";
            var endTitleTag = "</h2>";

            return ExtractTitle(url, checkTag, beginTitleTag, endTitleTag);
        }

        public string ExtractYoutubeTitle(string url)
        {
            var checkTag = "<div id=\"content\"";
            var beginTitleTag = "<meta itemprop=\"name\" content=\"";
            var endTitleTag = "\">";

            return ExtractTitle(url, checkTag, beginTitleTag, endTitleTag);
        }

        private string ExtractTitle(string url, string checkTag, string beginTitleTag, string endTitleTag)
        {
            var webData = webClient.DownloadString(url);
            var beginPosition = webData.IndexOf(checkTag, StringComparison.InvariantCulture);
            if (beginPosition < 0) return null;
            beginPosition = webData.IndexOf(beginTitleTag, beginPosition, StringComparison.InvariantCulture);
            if (beginPosition < 0) return null;
            beginPosition += beginTitleTag.Length;
            var endPosition = webData.IndexOf(endTitleTag, beginPosition, StringComparison.InvariantCulture);
            if (endPosition < 0) return null;
            var length = endPosition - beginPosition;
            var result = webData.Substring(beginPosition, length);

            result = RemoveSpecialCharacters(result);
            return result;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            var result = Regex.Replace(str, "[^a-zA-Z0-9_. -:+=,\\[\\]]+", "", RegexOptions.Compiled);
            result = Regex.Replace(result, "(&#39)", "'", RegexOptions.Compiled);
            return Regex.Replace(result, "(&quot)", "\"", RegexOptions.Compiled);
        }
    }
}
