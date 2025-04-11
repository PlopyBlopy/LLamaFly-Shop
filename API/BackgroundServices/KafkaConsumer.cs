using API.Interfaces;
using Confluent.Kafka;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace API.BackgroundServices
{
    public class KafkaConsumer<TMessage, TService> : BackgroundService, IKafkaConsumer<TMessage> where TService : IServiceConsumer
    {
        private readonly IOptionsMonitor<KafkaConsumerOptions> _optionsMonitor;
        private readonly string _configurationName;
        private readonly ILogger<KafkaConsumer<TMessage, TService>> _logger;
        private IConsumer<string, string> _consumer;
        private readonly TService _service;

        public string Topic => _optionsMonitor.Get(_configurationName).Topic;

        public KafkaConsumer(IOptionsMonitor<KafkaConsumerOptions> optionsMonitor, string configurationName, ILogger<KafkaConsumer<TMessage, TService>> logger, TService service)
        {
            _optionsMonitor = optionsMonitor;
            _configurationName = configurationName;
            _logger = logger;
            _service = service;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => StartConsuming(stoppingToken), stoppingToken);
        }

        private void StartConsuming(CancellationToken stoppingToken)
        {
            var options = _optionsMonitor.Get(_configurationName);
            ValidateOptions(options);

            var config = new ConsumerConfig
            {
                BootstrapServers = options.BootstrapServers,
                GroupId = options.GroupId,
                AutoOffsetReset = options.AutoOffsetReset,
                //EnableAutoCommit = false
            };

            _consumer = new ConsumerBuilder<string, string>(config)
                .SetErrorHandler(HandleKafkaError)
                .Build();

            _consumer.Subscribe(options.Topic);

            _logger.LogInformation("Started consumer for {Topic} ({ConfigName})",
                options.Topic, _configurationName);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);
                        ProcessMessage(result.Message.Value);
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, "Consume error: {Reason}", ex.Error.Reason);
                    }
                }
            }
            finally
            {
                _consumer.Close();
                _consumer.Dispose();
            }
        }

        private async void ProcessMessage(string message)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<TMessage>(message);
                // TODO: Реальная обработка сообщения
                // TODO: обработать ошибку равная сигнатуре Result<Guid>  (от FluentResult)
                var result = await _service.AddConsumerProfileMessage(obj);

                if (result.IsFailed)
                {
                    // TODO: (SAGA) Вызвать метод для отправки сообщения в Kafka с ошибкой валидации, так же профиль не добавлен
                }

                _logger.LogDebug("Processed message of type {Type}", typeof(TMessage).Name);
            }
            catch (Exception ex)
            {
                // TODO: (SAGA) Вызвать метод для отправки сообщения в Kafka с ошибкой валидации, так же профиль не добавлен

                _logger.LogError(ex, "Error processing message");
            }
        }

        private void ValidateOptions(KafkaConsumerOptions options)
        {
            if (string.IsNullOrEmpty(options.BootstrapServers))
                throw new ArgumentException("BootstrapServers must be configured");

            if (string.IsNullOrEmpty(options.Topic))
                throw new ArgumentException("Topic must be configured");

            if (string.IsNullOrEmpty(options.GroupId))
                throw new ArgumentException("GroupId must be configured");
        }

        private void HandleKafkaError(IConsumer<string, string> consumer, Error error)
        {
            _logger.LogError("Kafka error: {Reason} (Code: {Code})",
                error.Reason, error.Code);
        }

        public override void Dispose()
        {
            _consumer?.Dispose();
            base.Dispose();
        }
    }
}