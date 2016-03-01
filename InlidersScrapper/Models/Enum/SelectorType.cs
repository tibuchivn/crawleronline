using System.ComponentModel.DataAnnotations;

namespace InlidersScrapper.Models.Enum
{
    public enum SelectorType
    {
        [Display(Name = "By Id")]
        ById = 1,
        [Display(Name = "By Class")]
        ByClass,
    }
}