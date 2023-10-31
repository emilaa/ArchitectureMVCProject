using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.ProductCategory
{
    public class ProductCategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
    }
}
