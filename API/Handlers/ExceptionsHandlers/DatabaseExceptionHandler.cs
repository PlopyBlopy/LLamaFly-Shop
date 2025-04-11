using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;

namespace API.Handlers.ExceptionsHandlers
{
    public class DatabaseExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<DatabaseExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public DatabaseExceptionHandler(ILogger<DatabaseExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not PostgresException ex)
                return false;

            _logger.LogError(ex,
                "Database error: {Message} [SQL State: {SqlState}]",
                ex.Message,
                ex.SqlState);

            var problemDetails = CreateProblemDetails(httpContext, ex);
            await WriteResponse(httpContext, problemDetails, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(HttpContext httpContext, PostgresException ex)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path,
                Extensions =
                {
                    ["traceId"] = httpContext.TraceIdentifier,
                    ["timestamp"] = DateTime.Now.ToString("O"),
                    ["sqlState"] = ex.SqlState
                }
            };

            switch (ex.SqlState)
            {
                case "23505": // Unique violation
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
                    problemDetails.Title = "Conflict";
                    problemDetails.Status = (int)HttpStatusCode.Conflict;
                    problemDetails.Detail = ExtractConstraintMessage(ex);
                    break;

                case "23503": // Foreign key violation
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                    problemDetails.Title = "Invalid Reference";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Detail = "Referenced entity does not exist";
                    break;

                //case "40001": // Deadlock
                case "40P01": // Lock not available
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                    problemDetails.Title = "Service Unavailable";
                    problemDetails.Status = (int)HttpStatusCode.ServiceUnavailable;
                    problemDetails.Detail = "Resource conflict, please retry";
                    problemDetails.Extensions["retryAfter"] = 1; // секунды
                    break;

                default:
                    problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                    problemDetails.Title = "Database Error";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = _env.IsDevelopment()
                        ? ex.Message
                        : "Database operation failed";
                    break;
            }

            return problemDetails;
        }

        private static async Task WriteResponse(HttpContext httpContext, ProblemDetails problemDetails, CancellationToken ct)
        {
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);
        }

        private static string? ExtractConstraintMessage(PostgresException ex)
        {
            return ex.ConstraintName switch
            {
                "uq_products_title" => "Product with this title already exists",
                "fk_products_sellers" => "Specified seller does not exist",
                _ => ex.Message
            };
        }
    }
}