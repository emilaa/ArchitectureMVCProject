using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {

            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _homeService.GetAllAsync();
            return View(model);
        }


    }
}