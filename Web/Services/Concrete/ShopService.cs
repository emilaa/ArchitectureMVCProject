using Core.Entities;
using DataAccess.Migrations;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.AccessControl;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Services.Concrete
{
    public class ShopService : IShopService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ShopService(IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }




        public async Task<int> GetPageCountAsync(IQueryable<Doctor> doctors, int take)
        {
            var pageCount = await doctors.CountAsync();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }


        public async Task<ShopIndexVM> GetAllProductsWithCategoriesAsync(ShopIndexVM model)
        {
            var products = await _productRepository.FilterByName(model.Name);
            products = await _productRepository.FilterByCategoryId(products, model.CategoryId);
            var pageCount = await _productRepository.GetPageCountAsync(products, model.Take);
            products = await _productRepository.PaginateProductAsync(products, model.Page, model.Take);

            model = new ShopIndexVM
            {
                Products = products.ToList(),
                Categories = await _productCategoryRepository.GetCategoryWithProduct(),
                PageCount = pageCount,
                Page = model.Page,
                Take = model.Take,
                Name = model.Name,
            };
            return model;
        }
    }
}
