using System.ComponentModel.DataAnnotations;

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

        public string ReturnUrl { get; set; }
    }
}