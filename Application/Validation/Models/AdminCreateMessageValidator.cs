using Core.Contracts.Messages;
using FluentValidation;

namespace Application.Validation.Models
{
    public class AdminCreateMessageValidator : AbstractValidator<AdminCreateMessage>
    {
        public AdminCreateMessageValidator(bool canNull = false)
        {
        }
    }
}