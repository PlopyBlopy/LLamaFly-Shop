using Core.Contracts.Messages;
using Core.Interfaces;
using Core.Options;
using Infrastructure.Kafka.Producers;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class KafkaProducerExtensions
    {
        public static IServiceCollection AddKafkaProducersServices(this IServiceCollection services, IConfiguration configuration
            )
        {
            services.AddProducer<UserProfileAdminCreateMessage>(configuration.GetSection("Kafka:Producer:AdminCreate"), "AdminCreate");
            services.AddProducer<UserProfileSellerCreateMessage>(configuration.GetSection("Kafka:Producer:SellerCreate"), "SellerCreate");
            services.AddProducer<UserProfileCustomerCreateMessage>(configuration.GetSection("Kafka:Producer:CustomerCreate"), "CustomerCreate");

            return services;
        }

        public static IServiceCollection AddProducer<TMessage>(this IServiceCollection services, IConfigurationSection configurationSection, string configurationName)
        {
            services.Configure<KafkaOptions>(configurationName, configurationSection);

            services.AddSingleton<IKafkaProducer<TMessage>>(provider =>
            {
                var optionsMonitor = provider.GetRequiredService<IOptionsMonitor<KafkaOptions>>();
                return new KafkaProducer<TMessage>(optionsMonitor, configurationName);
            });

            return services;
        }
    }
}