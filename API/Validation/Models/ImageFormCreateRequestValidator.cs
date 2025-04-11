using API.Contracts.Requests;
using API.Validation.Properties;
using FluentValidation;

namespace API.Validation.Models
{
    public class ImageFormCreateRequestValidator : AbstractValidator<ImageFormRequest>
    {
        public ImageFormCreateRequestValidator()
        {
            RuleFor(x => x.Image.ContentType).SetValidator(new ImageContentTypeValidator());
            RuleFor(x => x.Image.Length).SetValidator(new ImageLengthValidator());
            RuleFor(x => x.Order).SetValidator(new ImageOrderValidator());
        }
    }
}
