using Application.Validation.Models;
using Core.Contracts.Messages;
using Core.Contracts.Requests;
using FluentValidation;

namespace API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserRegisterRequest>, UserRegisterRequestValidator>();

            return services;
        }
    }
}