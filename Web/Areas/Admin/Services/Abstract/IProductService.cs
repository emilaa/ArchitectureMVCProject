using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.ViewModels.Product;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM model);
        Task<ProductIndexVM> GetAllAsync();
        Task<ProductIndexVM> GetProductsWithCategoryAsync();
        Task<bool> UpdateAsync(ProductUpdateVM model);
        Task<List<SelectListItem>> GetCategoriesAsync();
        public Task<ProductCreateVM> GetCategoriesCreateModelAsync();
        Task<ProductUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);


    }
}
