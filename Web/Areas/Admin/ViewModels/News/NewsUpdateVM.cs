using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.LastestNews
{
    public class NewsUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Order { get; set; }
        public IFormFile? Photo { get; set; }
        [Required]
        public string Type { get; set; }
        
    }
}
