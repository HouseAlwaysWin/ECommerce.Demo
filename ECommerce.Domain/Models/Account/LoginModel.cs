namespace ECommerce.Domain.Models.Account
{

    public class LoginModel
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public bool LockoutOnFailure { get; set; }
    }
}