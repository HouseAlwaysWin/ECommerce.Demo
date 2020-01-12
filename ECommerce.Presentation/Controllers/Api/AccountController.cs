using System.Threading.Tasks;
using ECommerce.Domain.Models;
using ECommerce.Domain.ViewModels;
using ECommerce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers.Api
{

    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            var result = await _accountService.LoginAsync(new LoginModel
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe,
                LockoutOnFailure = false,
            });

            switch (result.Status)
            {
                case LoginStatus.Success:
                    return Ok(new
                    {
                        isSuccessed = true,
                        RedirectUrl = model.ReturnUrl
                    });
                case LoginStatus.TwoFactor:
                    return Ok(new
                    {
                        isSuccessed = true,
                        RedirectUrl = Url.Content("~/LoginWith2fa")
                    });
                case LoginStatus.LockOut:
                    return Ok(new
                    {
                        isSuccessed = true,
                        RedirectUrl = Url.Content("~/Lockout")
                    });
                case LoginStatus.Fail:
                default:
                    return BadRequest(new
                    {
                        isSuccess = false,
                        Message = result.Message,
                        redirectUrl = Url.Content("~/")
                    });
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
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
                case RegisterStatus.RequireConfirmedAccount:
                    return Ok(new
                    {
                        isSuccess = true,
                        redirectUrl = model.ReturnUrl
                    });
                case RegisterStatus.Fail:
                    return BadRequest(new
                    {
                        isSuccess = false,
                        redirectUrl = model.ReturnUrl
                    });
            }

            return BadRequest(new
            {
                isSuccess = false,
                redirectUrl = model.ReturnUrl
            });
        }
    }
}