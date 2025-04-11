using Core.Interfaces;

namespace Core.Options
{
    public class KafkaOptions
    {
        public string BootstrapServers { get; init; } = string.Empty;
        public string Topic { get; init; } = string.Empty;
    }
}