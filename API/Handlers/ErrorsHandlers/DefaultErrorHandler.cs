using API.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers.ErrorsHandlers
{
    public class DefaultErrorHandler : IErrorHandler
    {
        private readonly IWebHostEnvironment _env;

        public DefaultErrorHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ActionResult Handle(IError error)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1", // RFC для 500
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = _env.IsDevelopment() ? error.Message : "An error occurred on the server.",
                Extensions =
                {
                    ["errorCode"] = error.Metadata.TryGetValue("errorCode", out var code) ? code : "UnknownError"
                }
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
