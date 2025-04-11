using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class PhoneNumberValidator : AbstractValidator<string>, IUserConstraints
    {
        private readonly int minLength = IUserConstraints.MIN_PHONENUMBER_LENGTH;
        private readonly int maxLength = IUserConstraints.MAX_PHONENUMBER_LENGTH;

        public PhoneNumberValidator()
        {
            RuleFor(name => name)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}