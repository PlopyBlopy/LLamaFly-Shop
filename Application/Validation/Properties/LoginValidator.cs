using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class LoginValidator : AbstractValidator<string>, IUserConstraints
    {
        private readonly int minLength = IUserConstraints.MIN_LOGIN_LENGTH;
        private readonly int maxLength = IUserConstraints.MAX_LOGIN_LENGTH;

        public LoginValidator()
        {
            RuleFor(name => name)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}