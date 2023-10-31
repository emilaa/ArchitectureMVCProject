using Core.Utilities.Attributes;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Login
        [OnlyAnonimous]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [OnlyAnonimous]
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceded = await _accountService.LoginAsync(model);
            if (!string.IsNullOrEmpty(model.ReturnUrl)) return Redirect(model.ReturnUrl);
            if (isSucceded) return RedirectToAction("index", "home");
            return View(model);
        }
        #endregion

        #region Register
        [OnlyAnonimous]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [OnlyAnonimous]
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (await _accountService.RegisterAsync(model)) return RedirectToAction(nameof(Login));
            return View(model);
        }
        #endregion

        #region LogOut       
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
