using ECommerce.Domain.Models.Account;
using FluentValidation;

namespace ECommerce.Service.ModelValidations
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}