using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.Domain.Models.Account
{


    public class RegisterModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }

        public bool UseEmailVerified { get; set; }


    }
}