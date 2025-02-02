using Application.Validation.Properties;
using Core.Contracts.Dto;
using FluentValidation;

namespace Application.Validation.Models
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Title).SetValidator(new ProductTitleValidator());
            RuleFor(x => x.Description).SetValidator(new ProductDescriptionValidation());
            RuleFor(x => x.Price).SetValidator(new ProductPriceValidator());
            RuleFor(x => x.Rating).SetValidator(new ProductRatingValidator());
        }
    }
}