using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IActionContextAccessor actionContextAccessor,
            IProductCategoryRepository productCategoryRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<bool> CreateAsync(ProductCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _productCategoryRepository.AnyAsync(ct => ct.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This category already created");
                return false;
            }

            var category = new ProductCategory
            {
                CreatedAt = DateTime.Now,
                Title = model.Title,
            };
            await _productCategoryRepository.CreateAsync(category);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _productCategoryRepository.GetAsync(id);
            await _productCategoryRepository.DeleteAsync(category);
        }

        public async Task<ProductCategoryIndexVM> GetAllAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            var model = new ProductCategoryIndexVM { Categories = categories };
            return model;

        }

        public async Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _productCategoryRepository.GetAsync(id);
            var model = new ProductCategoryUpdateVM { Title = category.Title };
            return model;
        }

        public async Task<bool> UpdateAsync(ProductCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _productCategoryRepository.AnyAsync(ct => ct.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != ct.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This category already created");
                return false;
            }
            var category = await _productCategoryRepository.GetAsync(model.Id);
            category.Title = model.Title;
            category.ModifiedAt = DateTime.Now;

            await _productCategoryRepository.UpdateAsync(category);
            return true;
        }
    }
}
