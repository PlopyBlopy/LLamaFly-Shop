using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductTitleValidator : AbstractValidator<string>, IProductTitleConstraint
    {
        private readonly int minLength = IProductTitleConstraint.MIN_TITLE_LENGTH;
        private readonly int maxLength = IProductTitleConstraint.MAX_TITLE_LENGTH;

        public ProductTitleValidator()
        {
            RuleFor(title => title)
                .NotEmpty().WithMessage("Title is required.") // Проверка на пустую строку
                .Length(minLength, maxLength).WithMessage($"The title must contain at least {minLength} characters and no more than {maxLength}.");
        }
    }
}