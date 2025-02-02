using Application.Validation.Properties;
using Core.Contracts.Dto;
using FluentValidation;

namespace Application.Validation.Models
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Title).SetValidator(new CategoryTitleValidator());
        }
    }
}