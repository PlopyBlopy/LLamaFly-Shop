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
            services.AddScoped<IValidator<ProfileCreateMessage>, ProfileCreateMessageValidator>();
            services.AddScoped<IValidator<ProfileUpdateRequest>, ProfileUpdateRequestValidator>();

            //services.AddScoped<IValidator<AdminCreateMessage>, AdminCreateMessageValidator>();

            //services.AddScoped<IValidator<SellerCreateMessage>, SellerCreateMessageValidator>();

            //services.AddScoped<IValidator<CustomerCreateMessage>, CustomerCreateMessageValidator>();

            return services;
        }
    }
}