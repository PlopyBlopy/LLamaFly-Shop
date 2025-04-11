using Core.Interfaces.Constraints;
using FluentValidation;

namespace API.Validation.Properties
{
    public class ImageOrderValidator : AbstractValidator<int>, IImageOrderConstraint
    {
        private readonly int minOrder = IImageOrderConstraint.MIN_ORDER;
        private readonly int maxOrder = IImageOrderConstraint.MAX_ORDER;

        public ImageOrderValidator()
        {
            RuleFor(order => order)
                //.NotEmpty()
                .InclusiveBetween(minOrder, maxOrder).WithMessage($"The order of the image goes beyond the acceptable range from {minOrder} to {maxOrder}");
        }
    }
}