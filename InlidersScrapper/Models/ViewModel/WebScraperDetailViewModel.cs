using System;
using InlidersScrapper.Models.Enum;

namespace InlidersScrapper.Models.ViewModel
{
    public class WebScraperDetailViewModel
    {
        public int Id { get; set; }
        
        public string Url { get; set; }
        public string WebContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public SelectorType SelectorType { get; set; }
        public string SelectorValue { get; set; }
    }
}