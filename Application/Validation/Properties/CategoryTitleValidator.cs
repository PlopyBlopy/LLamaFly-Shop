using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class CategoryTitleValidator : AbstractValidator<string>, ICategoryTitleConstraint
    {
        private readonly int minLength = ICategoryTitleConstraint.MIN_TITLE_LENGTH;
        private readonly int maxLength = ICategoryTitleConstraint.MAX_TITLE_LENGTH;

        public CategoryTitleValidator()
        {
            RuleFor(title => title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(minLength, maxLength).WithMessage($"The title must contain at least {minLength} characters and no more than {maxLength}.");
        }
    }
}