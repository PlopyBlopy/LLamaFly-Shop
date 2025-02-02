using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductRatingValidator : AbstractValidator<double>, IProductRatingConstraint
    {
        private readonly double minValue = IProductRatingConstraint.MIN_RATING;
        private readonly double maxValue = IProductRatingConstraint.MAX_RATING;

        public ProductRatingValidator()
        {
            RuleFor(rating => rating)
                .NotEmpty().WithMessage("Rating is required")
                .InclusiveBetween(minValue, maxValue).WithMessage($"The rating must be between {minValue} and {maxValue}.");
        }
    }
}