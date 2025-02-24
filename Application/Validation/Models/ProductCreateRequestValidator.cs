using Application.Validation.Properties;
using Core.Contracts.Requests;
using FluentValidation;

namespace Application.Validation.Models
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(x => x.Title).SetValidator(new ProductTitleValidator());
            RuleFor(x => x.Description).SetValidator(new ProductDescriptionValidation());
            RuleFor(x => x.Price).SetValidator(new ProductPriceValidator());
        }
    }
}