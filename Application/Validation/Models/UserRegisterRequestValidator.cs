using Application.Validation.Properties;
using Core.Contracts.Requests;
using FluentValidation;

namespace Application.Validation.Models
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.Login).SetValidator(new LoginValidator());
            RuleFor(x => x.Email).SetValidator(new EmailValidator());
            RuleFor(x => x.PhoneNumber).SetValidator(new PhoneNumberValidator());
            RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        }
    }
}