using Application.Validation.Models;
using Core.Contracts.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(); // Вызов через IServiceCollection

            services.AddScoped<IValidator<ProductCreateRequest>, ProductCreateRequestValidator>();
            services.AddScoped<IValidator<CategoryCreateRequest>, CategoryCreateRequestValidator>();

            return services;
        }
    }
}