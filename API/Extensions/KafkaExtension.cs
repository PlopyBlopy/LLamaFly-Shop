using Core.Interfaces;
using Core.Options;
using Infrastructure.Kafka.Producers;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class KafkaExtension
    {
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