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
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }


        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _accountService.LogoutAsync();
            // _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }
}