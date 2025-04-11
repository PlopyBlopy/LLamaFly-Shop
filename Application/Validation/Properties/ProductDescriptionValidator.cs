using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductDescriptionValidator : AbstractValidator<string>, IProductConstraints
    {
        private readonly int minLength = IProductConstraints.MIN_Description_LENGTH;
        private readonly int maxLength = IProductConstraints.MAX_Description_LENGTH;

        public ProductDescriptionValidator()
        {
            RuleFor(description => description)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}