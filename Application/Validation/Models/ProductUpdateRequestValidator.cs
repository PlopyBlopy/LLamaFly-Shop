using Application.Validation.Properties;
using Core.Contracts.Requests;
using Core.Interfaces;
using FluentValidation;

namespace Application.Validation.Models
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Title).SetValidator(new ProductTitleCanNullValidator());
            RuleFor(x => x.Description).SetValidator(new ProductDescriptionCanNullValidation());
            RuleFor(x => x.Price).SetValidator(new ProductPriceCanNullValidator());
            //RuleFor(x => x.CategoryId).SetValidator(new ProductCategoryCanNullValidator(categoryRepository));
        }
    }
}