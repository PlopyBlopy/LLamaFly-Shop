using API.Interfaces;
using Core.Extensions.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers.ErrorsHandlers
{
    public class ValidationRangeErrorHandler : IErrorHandler
    {
        public ActionResult Handle(IError error)
        {
            var validationError = (ValidationRangeError)error;
            var fieldErrors = new Dictionary<string, List<object>>();  // Изменили на List<object>

            foreach (var errorList in validationError.ReasonsRange)
            {
                foreach (var e in errorList.OfType<ValidationFieldError>())
                {
                    var propName = e.Metadata["propertyName"].ToString();
                    var errorDetails = new
                    {
                        errorCode = e.Metadata["errorCode"],
                        message = e.Message,
                        attemptedValue = e.Metadata["attemptedValue"],
                    };

                    if (!fieldErrors.ContainsKey(propName))
                    {
                        fieldErrors[propName] = new List<object>();
                    }
                    fieldErrors[propName].Add(errorDetails);
                }
            }

            var problemDetails = new
            {
                type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                title = validationError.Message,
                status = 400,
                errorCode = validationError.Metadata["errorCode"],
                errors = fieldErrors
            };

            return new BadRequestObjectResult(problemDetails);
        }
    }
}