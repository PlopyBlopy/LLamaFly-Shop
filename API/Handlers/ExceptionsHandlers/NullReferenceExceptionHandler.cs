using Microsoft.AspNetCore.Diagnostics;

namespace API.Handlers.ExceptionsHandlers
{
    public class NullReferenceExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}