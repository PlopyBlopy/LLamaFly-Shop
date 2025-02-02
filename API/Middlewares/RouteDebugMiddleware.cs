namespace API.Middleware
{
    public class RouteDebugMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EndpointDataSource _endpointDataSource;

        public RouteDebugMiddleware(RequestDelegate next, EndpointDataSource endpointDataSource)
        {
            _next = next;
            _endpointDataSource = endpointDataSource;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoints = _endpointDataSource.Endpoints;
            foreach (var endpoint in endpoints)
            {
                if (endpoint is RouteEndpoint routeEndpoint)
                {
                    var routePattern = routeEndpoint.RoutePattern.RawText;
                    var httpMethods = routeEndpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods;
                    Console.WriteLine($"Route: {routePattern}, Methods: {string.Join(", ", httpMethods ?? new[] { "Any" })}");
                }
            }

            await _next(context);
        }
    }
}