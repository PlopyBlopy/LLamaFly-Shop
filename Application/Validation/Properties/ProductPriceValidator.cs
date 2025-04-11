using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductPriceValidator : AbstractValidator<decimal>, IProductConstraints
    {
        private readonly decimal minValue = IProductConstraints.MIN_PRICE;
        private readonly decimal maxValue = IProductConstraints.MAX_PRICE;

        public ProductPriceValidator()
        {
            RuleFor(price => price)
                .NotNull().WithMessage(BaseErrors.NotNull)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .InclusiveBetween(minValue, maxValue).WithMessage(BaseErrors.ValueBetween(minValue, maxValue));
        }
    }
}