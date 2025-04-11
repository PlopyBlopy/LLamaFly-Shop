using Core.Interfaces.Constraints;
using FluentValidation;

namespace API.Validation.Properties
{
    public class ImageLengthValidator : AbstractValidator<long>, IImageLengthConstraint
    {
        private readonly long _maxLength = IImageLengthConstraint.MAX_LENGTH;
        private readonly string _maxLengthUnit = IImageLengthConstraint.MAX_LENGTH_UNIT;

        public ImageLengthValidator()
        {
            RuleFor(length => length)
                .LessThanOrEqualTo(_maxLength)
                .WithMessage($"The image size is more than {_maxLengthUnit}");
        }
    }
}