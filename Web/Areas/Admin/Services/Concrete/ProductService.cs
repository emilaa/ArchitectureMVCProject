using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public ProductService(IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileService)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            model.Categories = await GetCategoriesAsync();
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This product already created");
                return false;
            }
            int maxSize = 1000;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be image");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb");
                return false;
            }
            var product = new Product
            {
                CreatedAt = DateTime.Now,
                CategoryId = model.CategoryId,
                Photoname = await _fileService.UploadAsync(model.Photo),
                Price = model.Price,
                Title = model.Title,
                Quantity = model.Quantity,
            };

            await _productRepository.CreateAsync(product);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            _fileService.Delete(product.Photoname);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            return categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task<ProductCreateVM> GetCategoriesCreateModelAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList()
            };
            return model;
        }

        public async Task<ProductIndexVM> GetProductsWithCategoryAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetProductsWithCategoryAsync()
            };
            return model;
        }

        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            var model = new ProductUpdateVM
            {
                CategoryId = product.CategoryId,
                Categories = await GetCategoriesAsync(),
                Price = product.Price,
                Title = product.Title,
                Id = product.Id,
                Quantity = product.Quantity
            };
            return model;
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            model.Categories = await GetCategoriesAsync();
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != p.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This product already created");
                return false;
            }
            var product = await _productRepository.GetAsync(model.Id);
            product.ModifiedAt = DateTime.Now;
            product.CategoryId = model.CategoryId;
            product.Price = model.Price;
            product.Title = model.Title;
            product.Quantity = model.Quantity;


            if (model.Photo != null)
            {
                int maxSize = 1000;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File must be image");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb");
                    return false;
                }
                _fileService.Delete(product.Photoname);
                product.Photoname = await _fileService.UploadAsync(model.Photo);
            }
            await _productRepository.UpdateAsync(product);
            return true;

        }
    }
}
