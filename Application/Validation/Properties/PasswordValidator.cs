using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class PasswordValidator : AbstractValidator<string>, IUserConstraints
    {
        private readonly int minLength = IUserConstraints.MIN_PASSWORD_LENGTH;
        private readonly int maxLength = IUserConstraints.MAX_PASSWORD_LENGTH;

        public PasswordValidator()
        {
            RuleFor(name => name)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}