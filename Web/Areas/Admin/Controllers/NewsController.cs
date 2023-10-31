using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.LastestNews;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _lastestNewsService;

        public NewsController(INewsService lastestNewsService)
        {
            _lastestNewsService = lastestNewsService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _lastestNewsService.GetAllAsync();
            return View(model);
        }

        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateVM model)
        {
            var isSucceded = await _lastestNewsService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "lastestnews");
            return View(model);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            var model = await _lastestNewsService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, NewsUpdateVM model)
        {
            var isSucceded = await _lastestNewsService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "lastestnews");
            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _lastestNewsService.DeleteAsync(id);
            return RedirectToAction("index", "lastestnews");
        }
        #endregion
    }

}
