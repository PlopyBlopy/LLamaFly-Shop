using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class AuthenticationSettingsExtension
    {
        public static IServiceCollection AddAuthenticationSettingsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = configuration["Jwt:Issuer"],
                           ValidAudience = configuration["Jwt:Audience"],
                           IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                       };
                   });
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        RoleClaimType = "role",
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidAudience = configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
            //    };
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            Console.WriteLine($"Authentication failed: {context.Exception}");
            //            return Task.CompletedTask;
            //        },
            //        OnTokenValidated = context =>
            //        {
            //            var claims = context.Principal.Claims;
            //            foreach (var claim in claims)
            //            {
            //                Console.WriteLine($"{claim.Type}: {claim.Value}");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        RoleClaimType = "role",
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidAudience = configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
            //    };
            //});

            return services;
        }
    }
}