using System.Threading.Tasks;
using ECommerce.Domain.Models;
using ECommerce.Domain.ViewModels;
using ECommerce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers.Api
{
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {

            var registerResult = await _accountService.RegisterAsync(new RegisterModel
            {
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                ReturnUrl = model.ReturnUrl,
                UseEmailVerified = false
            });

            switch (registerResult.Status)
            {
                case RegisterStatus.Success:
                    return LocalRedirect(model.ReturnUrl);
                case RegisterStatus.RequireConfirmedAccount:
                    return RedirectToPage("RegisterConfirmation", new { email = model.Email });
                case RegisterStatus.Fail:
                    return NotFound();
            }

            return NotFound();
        }
    }
}