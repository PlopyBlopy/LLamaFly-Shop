using API.Interfaces;
using Core.Extensions.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Handlers.ErrorsHandlers
{
    public class NotFoundErrorHandler : IErrorHandler
    {
        public ActionResult Handle(IError error)
        {
            var notFoundError = (NotFoundError)error;
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                Title = notFoundError.Message,
                Status = (int)HttpStatusCode.NotFound,
                Extensions =
                {
                    ["errorCode"] = notFoundError.Metadata["errorCode"],
                    ["entity"] = notFoundError.Metadata["entity"],
                }
            };

            if (notFoundError.Metadata.TryGetValue("key", out var key))
                problemDetails.Extensions["key"] = key;

            return new NotFoundObjectResult(problemDetails);
        }
    }
}
