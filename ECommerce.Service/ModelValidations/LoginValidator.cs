using ECommerce.Domain.Models;
using FluentValidation;

namespace ECommerce.Service.ModelValidations {
    public class LoginValidator : AbstractValidator<LoginModel> {
        public LoginValidator () {
            RuleFor (x => x.Email).NotEmpty ();
            RuleFor (x => x.Password).NotEmpty ();
            RuleFor (x => x.RememberMe).NotEmpty ();
            RuleFor (x => x.LockoutOnFailure).NotEmpty ();
        }
    }
}