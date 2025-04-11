using Core.Extensions.Errors;
using Core.Interfaces.Constraints;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class CategoryTitleValidator : AbstractValidator<string>, ICategoryConstraints
    {
        private readonly int minLength = ICategoryConstraints.MIN_TITLE_LENGTH;
        private readonly int maxLength = ICategoryConstraints.MAX_TITLE_LENGTH;

        public CategoryTitleValidator()
        {
            RuleFor(title => title)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .Length(minLength, maxLength).WithMessage(BaseErrors.CharactersLength(minLength, maxLength));
        }
    }
}