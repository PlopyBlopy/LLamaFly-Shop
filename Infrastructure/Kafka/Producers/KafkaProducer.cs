using Confluent.Kafka;
using Core.Interfaces;
using Core.Options;
using Infrastructure.Kafka.Serializers;
using Microsoft.Extensions.Options;

namespace Infrastructure.Kafka.Producers
{
    public class KafkaProducer<TMessage> : IKafkaProducer<TMessage>
    {
        private readonly KafkaOptions _options;
        private readonly IProducer<string, TMessage> _producer;
        private readonly string _topic;

        public KafkaProducer(IOptionsMonitor<KafkaOptions> optionsMonitor, string configurationName)
        {
            _options = optionsMonitor.Get(configurationName);

            //TODO: Validation
            //if (string.IsNullOrEmpty(_options.BootstrapServers))
            //    throw new ArgumentException("BootstrapServers is not set.");

            //if (string.IsNullOrEmpty(_options.Topic))
            //    throw new ArgumentException("Topic is not set.");

            var config = new ProducerConfig
            {
                BootstrapServers = _options.BootstrapServers
            };

            _producer = new ProducerBuilder<string, TMessage>(config)
                .SetValueSerializer(new JsonSerializer<TMessage>())
                .Build();

            _topic = _options.Topic;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task ProduceAsync(string key, TMessage message, CancellationToken ct)
        {
            await _producer.ProduceAsync(_topic, new Message<string, TMessage>
            {
                Key = key,
                Value = message
            }, ct);
        }
    }
}