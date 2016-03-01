using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using InlidersScrapper.Models.Enum;

namespace InlidersScrapper.Helper.Scraper
{
    public class CsQueryWebScraper : WebScraper
    {
        public override IList<string> Parse(SelectorType selectorType, string selectorValue)
        {
            var html = DownloadHtmlContent();
            CQ dom = html;
            string selector = string.Empty;
            switch (selectorType)
            {
                case SelectorType.ById:
                    selector = string.Format("#{0}", selectorValue);
                    break;
                case SelectorType.ByClass:
                    selector = string.Format(".{0}", selectorValue);
                    break;
            }
            if (!string.IsNullOrEmpty(selector))
            {
                return dom[selector].Select(x => x.InnerHTML).ToList();
            }
            return new List<string>();
        }

        public CsQueryWebScraper(string url) : base(url)
        {
        }
    }
}