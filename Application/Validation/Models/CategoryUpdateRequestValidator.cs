using Application.Validation.Properties;
using Core.Contracts.Requests;
using FluentValidation;

namespace Application.Validation.Models
{
    public class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequest>
    {
        public CategoryUpdateRequestValidator()
        {
            RuleFor(x => x.Title).SetValidator(new CategoryTitleValidator());
        }
    }
}