using Application.Validation.Properties;
using Core.Contracts.Requests;
using Core.Extensions.Errors;
using Core.Interfaces;
using FluentValidation;

namespace Application.Validation.Models
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator(ICategoryRepository categoryRepository, IProfileService profileService)
        {
            RuleFor(x => x.Title).SetValidator(new ProductTitleValidator());
            RuleFor(x => x.Description).SetValidator(new ProductDescriptionValidator());
            RuleFor(x => x.Price).SetValidator(new ProductPriceValidator());
            //RuleFor(x => x.CategoryId).SetValidator(new ProductCategoryValidator(categoryRepository));
        }
    }
}