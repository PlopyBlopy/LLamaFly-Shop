using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductTitleValidator : AbstractValidator<string>, IProductConstraints
    {
        private readonly int minLength = IProductConstraints.MIN_TITLE_LENGTH;
        private readonly int maxLength = IProductConstraints.MAX_TITLE_LENGTH;

        public ProductTitleValidator()
        {
            RuleFor(title => title)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}