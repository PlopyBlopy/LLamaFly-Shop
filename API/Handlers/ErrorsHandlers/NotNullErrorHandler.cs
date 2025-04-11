using API.Interfaces;
using Core.Extensions.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Handlers.ErrorsHandlers
{
    public class NotNullErrorHandler : IErrorHandler
    {
        public ActionResult Handle(IError error)
        {
            var notNullError = (NotNullError)error;
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = notNullError.Message,
                Status = (int)HttpStatusCode.BadRequest,
                Extensions =
                {
                    ["errorCode"] = notNullError.Metadata["errorCode"],
                    ["entity"] = notNullError.Metadata["entity"],
                }
            };

            if (notNullError.Metadata.TryGetValue("key", out var key))
                problemDetails.Extensions["key"] = key;

            return new BadRequestObjectResult(problemDetails);
        }
    }
}
