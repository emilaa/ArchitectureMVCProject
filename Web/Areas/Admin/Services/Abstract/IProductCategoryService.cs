using Web.Areas.Admin.ViewModels.ProductCategory;
using Web.Areas.Admin.ViewModels.QuestionCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductCategoryService
    {
        Task<bool> CreateAsync(ProductCategoryCreateVM model);
        Task<ProductCategoryIndexVM> GetAllAsync();
        Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(ProductCategoryUpdateVM model);
        Task DeleteAsync(int id);
    }
}
