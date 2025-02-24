using Core.Contracts.Dtos;

namespace API.Extensions
{
    public static class KafkaProducerExtensions
    {
        public static IServiceCollection AddKafkaProducersServices(this IServiceCollection services, IConfiguration configuration
            )
        {
            services.AddProducer<UserAdminProfileDto>(configuration.GetSection("Kafka:AdminProfile"), "AdminProfile");
            services.AddProducer<UserSellerProfileDto>(configuration.GetSection("Kafka:SellerProfile"), "SellerProfile");
            services.AddProducer<UserCustomerProfileDto>(configuration.GetSection("Kafka:CustomerProfile"), "CustomerProfile");

            return services;
        }
    }
}