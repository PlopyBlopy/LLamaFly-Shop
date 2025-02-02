using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductDescriptionValidation : AbstractValidator<string>, IProductDescriptionConstraint
    {
        private readonly int minLength = IProductDescriptionConstraint.MIN_Description_LENGTH;
        private readonly int maxLength = IProductDescriptionConstraint.MAX_Description_LENGTH;

        public ProductDescriptionValidation()
        {
            RuleFor(description => description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(minLength, maxLength).WithMessage($"The title must contain at least {minLength} characters and no more than {maxLength}.");
        }
    }
}