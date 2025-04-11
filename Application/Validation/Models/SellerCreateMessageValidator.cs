using Core.Contracts.Messages;
using FluentValidation;

namespace Application.Validation.Models
{
    public class SellerCreateMessageValidator : AbstractValidator<SellerCreateMessage>
    {
        public SellerCreateMessageValidator(bool canNull = false)
        {
        }
    }
}