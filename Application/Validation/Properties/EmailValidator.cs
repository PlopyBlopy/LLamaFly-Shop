using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class EmailValidator : AbstractValidator<string>, IUserConstraints
    {
        private readonly int minLength = IUserConstraints.MIN_EMAIL_LENGTH;
        private readonly int maxLength = IUserConstraints.MAX_EMAIL_LENGTH;

        public EmailValidator()
        {
            RuleFor(name => name)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}