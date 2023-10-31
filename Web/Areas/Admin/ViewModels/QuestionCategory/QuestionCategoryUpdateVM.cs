using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.QuestionCategory
{
    public class QuestionCategoryUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name = "Active Status")]
        public ActiveStatus ActiveStatus { get; set; }
    }
}
