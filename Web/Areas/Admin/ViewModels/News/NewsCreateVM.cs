using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.LastestNews
{
    public class NewsCreateVM
    {
        [Required]
        public string Title { get; set; }
        public int Order { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
