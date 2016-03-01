using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using InlidersScrapper.Models.Enum;

namespace InlidersScrapper.Helper.Scraper
{
    public abstract class WebScraper
    {
        protected string _url;
        public WebScraper(string url)
        {
            _url = url;
        }
        public abstract IList<string> Parse(SelectorType selectorType, string selectorValue);

        protected string DownloadHtmlContent()
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                return httpClient.GetStringAsync(new Uri(_url)).Result;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
            return httpClient;
        }
    }
}