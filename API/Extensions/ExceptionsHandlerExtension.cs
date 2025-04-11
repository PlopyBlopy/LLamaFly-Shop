using API.Handlers.ErrorsHandlers;
using API.Handlers.ExceptionsHandlers;
using API.Interfaces;

namespace API.Extensions
{
    public static class ExceptionsHandlerExtension
    {
        public static IServiceCollection AddExceptionsHandlerExtension(this IServiceCollection services)
        {
            services.AddScoped<IErrorFactoryHandler, ErrorFactoryHandler>();

            services.AddExceptionHandler<DatabaseExceptionHandler>();

            #region 400

            services.AddExceptionHandler<BadRequestExceptionHandler>();

            #endregion 400

            #region 500

            services.AddExceptionHandler<AutoMapperMappingExceptionHandler>();

            services.AddExceptionHandler<GlobalExceptionHandler>();

            #endregion 500

            return services;
        }
    }
}