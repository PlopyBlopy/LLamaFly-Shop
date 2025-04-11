namespace API.Interfaces
{
    public interface IKafkaConsumer<TMessage> : IHostedService, IDisposable
    {
        string Topic { get; }
    }
}