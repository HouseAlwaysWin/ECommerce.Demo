using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ECommerce.Domain.Models;
using ECommerce.Service.ModelValidations;
using ECommerce.Service.Services.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace ECommerce.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountService> _logger;
        public AccountService(
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountService> logger,
            UserManager<IdentityUser> userManager
        )
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        /// <summary>
        /// Register and Create new account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<RegisterResultModel> RegisterAsync(RegisterModel model)
        {
            RegisterResultModel result = new RegisterResultModel()
            {
                Status = RegisterStatus.Fail
            };

            try
            {
                var validator = new RegisterValidator();
                ValidationResult validationResult = validator.Validate(model);

                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var registerResult = await _userManager.CreateAsync(user, model.Password);

                if (registerResult.Errors.Count() != 0)
                {
                    var error = registerResult.Errors.FirstOrDefault();
                    result.Message = error.Description;
                    return result;
                }

                if (registerResult.Succeeded)
                {

                    _logger.LogInformation("User created a new account with password.");

                    if (model.UseEmailVerified)
                    {
                        // var code = await _userManager.GenerateEmailConfirmationTokenAsync (user);
                        // code = WebEncoders.Base64UrlEncode (Encoding.UTF8.GetBytes (code));
                        // var callbackUrl = model.ReturnUrl + $"?userId={user.Id}&code={code}";

                        // await _emailSender.SendEmailAsync (model.Email, "Confirm your email",
                        //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    }
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        result.Status = RegisterStatus.RequireConfirmedAccount;
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        result.Status = RegisterStatus.Success;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ECommerce.Service.Services.RegisterAsync() Exception:{ex}");
                return result;
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<LoginResultModel> LoginAsync(LoginModel model)
        {
            LoginResultModel result = new LoginResultModel()
            {
                Status = LoginStatus.Fail,
            };

            try
            {
                var validator = new LoginValidator();
                ValidationResult validationResult = validator.Validate(model);

                if (!validationResult.IsValid)
                {
                    result.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                    return result;
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, model.LockoutOnFailure);
                if (loginResult.Succeeded)
                {
                    result.Status = LoginStatus.Success;
                    result.Message = "Successed";
                }
                else if (loginResult.IsLockedOut)
                {
                    result.Status = LoginStatus.LockOut;
                    result.Message = "LockedOut";
                }
                else if (loginResult.RequiresTwoFactor)
                {
                    result.Status = LoginStatus.TwoFactor;
                    result.Message = "TwoFactor";
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"ECommerce.Service.Services.Login Exception:{ex} ");
                result.Status = LoginStatus.Fail;
                result.Message = "Failed";
                return result;
            }
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


    }
}