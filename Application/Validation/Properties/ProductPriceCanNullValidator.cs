using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductPriceCanNullValidator : AbstractValidator<decimal?>, IProductConstraints
    {
        private readonly decimal minValue = IProductConstraints.MIN_PRICE;
        private readonly decimal maxValue = IProductConstraints.MAX_PRICE;

        public ProductPriceCanNullValidator()
        {
            RuleFor(price => price)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .InclusiveBetween(minValue, maxValue).WithMessage(BaseErrors.ValueBetween(minValue, maxValue));
        }
    }
}