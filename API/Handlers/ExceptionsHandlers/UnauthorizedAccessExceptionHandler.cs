using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers.ExceptionsHandlers
{
    public class UnauthorizedAccessExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<UnauthorizedAccessExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public UnauthorizedAccessExceptionHandler(ILogger<UnauthorizedAccessExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not UnauthorizedAccessException ex)
            {
                return false;
            }

            _logger.LogWarning(ex, "Exception occurred: { Message }", ex.Message);

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Detail = ex.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}