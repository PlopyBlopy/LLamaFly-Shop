using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers.ExceptionsHandlers
{
    public class AutoMapperMappingExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AutoMapperMappingExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public AutoMapperMappingExceptionHandler(ILogger<AutoMapperMappingExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not AutoMapperMappingException ex)
            {
                return false;
            }

            _logger.LogError(exception, $"Exception occurred: {exception.Message}");

            var correlationId = httpContext.TraceIdentifier ?? "unknown";

            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Title = "An error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = _env.IsDevelopment() ? exception.Message : "An unexpected error has occurred.",
                Extensions =
                {
                    ["correlationId"] = correlationId,
                    ["timestamp"] = DateTime.Now.ToString()
                }
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}

//StatusCodes.Status500InternalServerError