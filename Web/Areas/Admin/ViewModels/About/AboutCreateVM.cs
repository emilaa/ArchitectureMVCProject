using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.AboutUs
{
    public class AboutCreateVM
    {
        [Required, MaxLength(14)]
        public string Header { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        public IFormFile IconPhoto { get; set; }
        [Required]
        public string Description { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
    }
}
