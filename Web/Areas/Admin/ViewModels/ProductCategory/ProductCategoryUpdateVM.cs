﻿using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.ProductCategory
{
    public class ProductCategoryUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
