using Application.Validation.Properties;
using Core.Contracts.Requests;
using FluentValidation;

namespace Application.Validation.Models
{
    public class ProfileUpdateRequestValidator : AbstractValidator<ProfileUpdateRequest>
    {
        public ProfileUpdateRequestValidator(bool canNull = false)
        {
            RuleFor(x => x.Name).SetValidator(new NameValidator(canNull));
            RuleFor(x => x.Surname).SetValidator(new SurnameValidator(canNull));
            RuleFor(x => x.Patronymic).SetValidator(new PatronymicValidator(canNull));
        }
    }
}