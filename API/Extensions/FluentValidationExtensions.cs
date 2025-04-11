using FluentValidation;
using FluentValidation.AspNetCore;
using API.Validation.Models;

namespace API.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); // Вызов через IServiceCollection
            services.AddFluentValidationClientsideAdapters(); // Поддержка клиентской валидации (если нужно)
            services.AddValidatorsFromAssemblyContaining<ImageFormCreateRequestValidator>();

            return services;
        }
    }
}