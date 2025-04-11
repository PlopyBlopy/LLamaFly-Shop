using Core.Interfaces.Constraints;
using FluentValidation;

namespace API.Validation.Properties
{
    public class ImageContentTypeValidator : AbstractValidator<string>, IImageContentTypeConstraint
    {
        private readonly string _type = IImageContentTypeConstraint.TYPE;

        public ImageContentTypeValidator()
        {
            RuleFor(contentType => contentType)
                .Must(x => IsValid(x))
                .WithMessage($"Invalid content type format. Expected: {_type}/{string.Join(" | ", Enum.GetNames<IImageContentTypeConstraint.ContentType>())}");
        }

        public bool IsValid(string input)
        {
            return input != null
                && input.StartsWith(_type + "/")
                && Enum.TryParse<IImageContentTypeConstraint.ContentType>(
                    input.AsSpan(_type.Length + 1),
                    ignoreCase: true,
                    out var contentType)
                && Enum.IsDefined(contentType);
        }
    }
}