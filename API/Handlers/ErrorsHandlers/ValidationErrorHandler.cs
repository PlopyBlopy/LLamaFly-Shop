using API.Interfaces;
using Core.Extensions.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers.ErrorsHandlers
{
    public class ValidationErrorHandler : IErrorHandler
    {
        public ActionResult Handle(IError error)
        {
            var validationError = (ValidationError)error;
            var fieldErrors = validationError.Reasons
                .OfType<ValidationFieldError>()
                .ToDictionary(
                    e => e.Metadata["propertyName"].ToString(),
                    e => new[] { e.Message }
                );

            return new BadRequestObjectResult(new ValidationProblemDetails(fieldErrors)
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = validationError.Message,
                Extensions = { ["errorCode"] = validationError.Metadata["errorCode"] }
            });
        }
    }
}
