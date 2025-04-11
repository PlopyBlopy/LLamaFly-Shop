using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class SurnameValidator : AbstractValidator<string>, IProfileConstraints
    {
        private readonly int minLength = IProfileConstraints.MIN_SURNAME_LENGTH;
        private readonly int maxLength = IProfileConstraints.MAX_SURNAME_LENGTH;

        public SurnameValidator(bool canNull)
        {
            if (canNull)
            {
                RuleFor(name => name)
                    .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                    .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
            }
            else
            {
                RuleFor(name => name)
                    .NotNull().WithMessage(BaseErrors.NotNull)
                    .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                    .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
            }
        }
    }
}