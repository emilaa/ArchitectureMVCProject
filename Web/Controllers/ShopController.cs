using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.Services.Concrete;
using Web.ViewModels.Faq;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        public async Task<IActionResult> Index(ShopIndexVM model)
        {
            model = await _shopService.GetAllProductsWithCategoriesAsync(model);
            return View(model);
        }


        //[HttpGet]
        //public async Task<IActionResult> FilterByCategory(int id)
        //{
        //    var products = await _shopService.Filter(id);
        //    var model = new ShopIndexVM
        //    {
        //        Products = products
        //    };
        //    return PartialView("_ProductPartial", model);
        //}

        //public async Task<IActionResult> FilterByName(string name)
        //{
        //    var products = await _shopService.FilterByName(name);
        //    var model = new ShopIndexVM
        //    {
        //        Products = products
        //    };
        //    return PartialView("_ProductPartial", model);
        //}
    }
}
