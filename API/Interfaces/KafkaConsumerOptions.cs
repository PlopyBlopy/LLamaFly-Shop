using Confluent.Kafka;

namespace API.Interfaces
{
    public class KafkaConsumerOptions
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
        public string GroupId { get; set; }
        public string ConfigurationName { get; set; }
        public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;
    }
}