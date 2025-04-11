using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class NameValidator : AbstractValidator<string>, IProfileConstraints
    {
        private readonly int minLength = IProfileConstraints.MIN_NAME_LENGTH;
        private readonly int maxLength = IProfileConstraints.MAX_NAME_LENGTH;

        public NameValidator(bool canNull)
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