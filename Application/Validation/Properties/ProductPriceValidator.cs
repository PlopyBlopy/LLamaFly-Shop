using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductPriceValidator : AbstractValidator<decimal>, IProductPriceConstraint
    {
        private readonly decimal minValue = IProductPriceConstraint.MIN_PRICE;
        private readonly decimal maxValue = IProductPriceConstraint.MAX_PRICE;

        public ProductPriceValidator()
        {
            RuleFor(price => price)
                .NotEmpty().WithMessage("Price is required.")
                .InclusiveBetween(minValue, maxValue).WithMessage($"The price must be between {minValue} and {maxValue}.");
        }
    }
}