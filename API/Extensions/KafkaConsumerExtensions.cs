using API.BackgroundServices;
using API.Interfaces;
using Core.Contracts.Messages;
using Core.Interfaces;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class KafkaConsumerExtensions
    {
        public static IServiceCollection AddKafkaConsumersServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConsumer<UserProfileAdminCreateMessage, IAdminService>(
            configuration.GetSection("kafka:Consumer:AdminCreate"),
            "AdminCreate");

            services.AddConsumer<UserProfileSellerCreateMessage, ISellerService>(
                configuration.GetSection("kafka:Consumer:SellerCreate"),
                "SellerProfileCreate");

            services.AddConsumer<UserProfileCustomerCreateMessage, ICustomerService>(
                configuration.GetSection("kafka:Consumer:CustomerCreate"),
                "CustomerProfileCreate");

            return services;
        }

        private static IServiceCollection AddConsumer<TMessage, TService>(this IServiceCollection services, IConfigurationSection configurationSection, string configurationName) where TService : IServiceConsumer
        {
            services.Configure<KafkaConsumerOptions>(configurationName, configurationSection);

            services.AddHostedService<IKafkaConsumer<TMessage>>(provider =>
            {
                var optionsMonitor = provider.GetRequiredService<IOptionsMonitor<KafkaConsumerOptions>>();
                var logger = provider.GetRequiredService<ILogger<KafkaConsumer<TMessage, TService>>>();

                TService service;
                using (var scope = provider.CreateScope())
                {
                    service = scope.ServiceProvider.GetRequiredService<TService>();
                }

                return new KafkaConsumer<TMessage, TService>(optionsMonitor, configurationName, logger, service);
            });

            return services;
        }
    }
}