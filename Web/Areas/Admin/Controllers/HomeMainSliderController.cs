using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;
namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeMainSliderController : Controller
    {
        private readonly IHomeMainSliderService _homeService;

        public HomeMainSliderController(IHomeMainSliderService homeService)
        {
            _homeService = homeService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _homeService.GetAllAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeMainSliderCreateVM model)
        {
            var isSucceded = await _homeService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "homemainslider");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeService.DeleteAsync(id);
            return RedirectToAction("index", "homemainslider");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeMainSliderUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            bool isSucceded = await _homeService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "homemainslider");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, HomeMainSliderDetailsVM model)
        {
            if (model.Id != id) return BadRequest();
            model = await _homeService.GetDetailsAsync(id);

            return View(model);
        }
    }
}
