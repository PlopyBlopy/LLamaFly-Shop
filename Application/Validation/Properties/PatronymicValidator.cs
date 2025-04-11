using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class PatronymicValidator : AbstractValidator<string>, IProfileConstraints
    {
        private readonly int minLength = IProfileConstraints.MIN_PATRONYMIC_LENGTH;
        private readonly int maxLength = IProfileConstraints.MAX_PATRONYMIC_LENGTH;

        public PatronymicValidator(bool canNull)
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