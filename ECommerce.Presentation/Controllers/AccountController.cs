using System;
using System.Threading.Tasks;
using ECommerce.Domain.Models;
using ECommerce.Domain.ViewModels;
using ECommerce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register () {
            return View (new RegisterViewModel ());
        }

        // [HttpPost]
        // public async Task<IActionResult> Register (RegisterViewModel model) {

        //     var registerResult = await _accountService.RegisterAsync (new RegisterModel {
        //         Email = model.Email,
        //             Password = model.Password,
        //             ConfirmPassword = model.ConfirmPassword,
        //             ReturnUrl = model.ReturnUrl,
        //             UseEmailVerified=false 
        //     });

        //     switch(registerResult.Status){
        //         case RegisterStatus.Success:
        //             return LocalRedirect(model.ReturnUrl);
        //         case RegisterStatus.RequireConfirmedAccount:
        //              return RedirectToPage("RegisterConfirmation", new { email = model.Email });
        //         case RegisterStatus.Fail:
        //             return View(); 
        //     }

        //     return View ();
        // }

        [HttpGet]
        public IActionResult Login () {
            return View (new LoginViewModel ());
        }

        // [HttpPost]
        // public async Task<IActionResult> Login (LoginViewModel model) {
        //     model.ReturnUrl = model.ReturnUrl ?? Url.Content ("~/");
        //     var result = await _accountService.LoginAsync (new LoginModel {
        //         Email = model.Email,
        //             Password = model.Password,
        //             RememberMe = model.RememberMe,
        //             LockoutOnFailure = false,
        //     });

        //     if (result.IsSuccess) {
        //         return LocalRedirect (model.ReturnUrl);
        //     }

        //     switch (result.Status) {
        //         case LoginStatus.TwoFactor:
        //             return RedirectToAction ("LoginWith2fa");
        //         case LoginStatus.LockOut:
        //             return RedirectToAction ("Lockout");

        //     }

        //     ModelState.AddModelError (string.Empty, result.Message);
        //     return View ();
        // }

    }
}