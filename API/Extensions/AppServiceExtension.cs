﻿using Application.Services;
using Core.Interfaces;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}