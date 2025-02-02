using Application.Validation.Models;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Services.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); // Вызов через IServiceCollection
            services.AddFluentValidationClientsideAdapters(); // Поддержка клиентской валидации (если нужно)

            services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>();

            return services;
        }
    }
}