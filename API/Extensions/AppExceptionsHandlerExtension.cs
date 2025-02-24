using API.Exceptions;

namespace API.Extensions
{
    public static class AppExceptionsHandlerExtension
    {
        public static IServiceCollection AddExceptionsHandlerExtension(this IServiceCollection services)
        {
            #region 400

            services.AddExceptionHandler<BadRequestExceptionHandler>();

            #endregion 400

            return services;
        }
    }
}