using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.Domain.ViewModels {
    public class RegisterViewModel {
        [Display (Name = "Username")]
        public string Username { get; set; }

        [Display (Name = "Email")]
        public string Email { get; set; }

        [Display (Name = "Password")]
        public string Password { get; set; }

        [Display (Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}