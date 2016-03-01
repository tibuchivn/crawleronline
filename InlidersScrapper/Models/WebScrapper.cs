using System;
using System.ComponentModel.DataAnnotations;

namespace InlidersScrapper.Models
{
    public class WebScrapper
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
        public string WebContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SelectorType { get; set; }
        public string SelectorValue { get; set; }
    }
}