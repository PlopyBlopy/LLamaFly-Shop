using Core.Contracts.Messages;
using FluentValidation;

namespace Application.Validation.Models
{
    public class CustomerCreateMessageValidator : AbstractValidator<CustomerCreateMessage>
    {
        public CustomerCreateMessageValidator(bool canNull = false)
        {
        }
    }
}