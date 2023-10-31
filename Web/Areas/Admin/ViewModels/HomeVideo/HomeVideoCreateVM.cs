using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.HomeVideo
{
    public class HomeVideoCreateVM
    {
        [Required]
        public string Link { get; set; }
        [Display(Name ="Cover Photo")]
        public IFormFile CoverPhoto { get; set; }
    }
}
