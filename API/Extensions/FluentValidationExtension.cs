using Application.Validation.Models;
using Core.Contracts.Requests;
using FluentValidation;

namespace API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            //services.AddFluentValidationAutoValidation();

            services.AddScoped<IValidator<ProductCreateRequest>, ProductCreateRequestValidator>();
            services.AddScoped<IValidator<ProductCreateWithRatingRequest>, ProductCreateWithRatingRequestValidator>();
            services.AddScoped<IValidator<ProductUpdateRequest>, ProductUpdateRequestValidator>();

            services.AddScoped<IValidator<CategoryCreateRequest>, CategoryCreateRequestValidator>();
            services.AddScoped<IValidator<CategoryUpdateRequest>, CategoryUpdateRequestValidator>();

            return services;
        }
    }
}