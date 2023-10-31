using Core.Entities;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Areas.Admin.ViewModels.HomeMainSlider
{
    public class HomeMainSliderCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slogan { get; set; }
        [Required]
        public string ButtonLink { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public int Order { get; set; }

    }
}
