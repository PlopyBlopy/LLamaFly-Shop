using Application.Validation.Properties;
using Core.Contracts.Requests;
using FluentValidation;

namespace Application.Validation.Models
{
    public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidator()
        {
            RuleFor(x => x.Title).SetValidator(new CategoryTitleValidator());
        }
    }
}