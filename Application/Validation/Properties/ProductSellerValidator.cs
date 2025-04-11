using Core.Extensions.Errors;
using Core.Interfaces;
using FluentValidation;

namespace Application.Validation.Properties
{
    public class ProductSellerValidator : AbstractValidator<Guid>
    {
        public ProductSellerValidator(IProfileService service)
        {
            RuleFor(sellerId => sellerId)
                .NotEmpty().WithMessage(BaseErrors.NotEmpty)
                .MustAsync(async (sellerId, ct) =>
                    await service.IsSellerExist(sellerId, ct)).WithMessage(BaseErrors.NotExist);
        }
    }
}