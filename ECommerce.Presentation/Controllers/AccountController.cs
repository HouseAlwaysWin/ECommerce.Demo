using System;
using System.Threading.Tasks;
using ECommerce.Domain.Models;
using ECommerce.Domain.ViewModels;
using ECommerce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers {
    public class AccountController : Controller {
        IAccountService _accountService;
        public AccountController (IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register () {
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> Register (RegisterViewModel model) {

            var registerResult = await _accountService.RegisterAsync (new RegisterModel {
                Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    ReturnUrl = model.ReturnUrl
            });
            return View ();
        }

        [HttpGet]
        public IActionResult Login () {
            return View (new LoginViewModel ());
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model) {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content ("~/");
            var result = await _accountService.LoginAsync (new LoginModel {
                Email = model.Email,
                    Password = model.Password,
                    RememberMe = model.RememberMe,
                    LockoutOnFailure = false,
            });

            if (result.IsSuccess) {
                return LocalRedirect (model.ReturnUrl);
            }

            switch (result.Type) {
                case LoginResultType.TwoFactor:
                    return RedirectToAction ("LoginWith2fa");
                case LoginResultType.LockOut:
                    return RedirectToAction ("Lockout");

            }

            ModelState.AddModelError (string.Empty, result.Message);
            return View ();
        }

    }
}