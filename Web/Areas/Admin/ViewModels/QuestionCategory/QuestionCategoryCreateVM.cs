using Core.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.QuestionCategory
{
    public class QuestionCategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name = "Active Status")]
        public ActiveStatus ActiveStatus { get; set; } 


    }
}
