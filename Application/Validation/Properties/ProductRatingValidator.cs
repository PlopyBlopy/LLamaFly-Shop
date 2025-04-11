using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductRatingValidator : AbstractValidator<double>, IProductConstraints
    {
        private readonly double minValue = IProductConstraints.MIN_RATING;
        private readonly double maxValue = IProductConstraints.MAX_RATING;

        public ProductRatingValidator()
        {
            RuleFor(rating => rating)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .InclusiveBetween(minValue, maxValue).WithMessage(BaseErrors.ValueBetween(minValue, maxValue));
        }
    }
}