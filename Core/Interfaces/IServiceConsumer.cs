using FluentResults;

namespace Core.Interfaces
{
    public interface IServiceConsumer
    {
        Task<Result<Guid>> AddConsumerProfileMessage<T>(T dto, CancellationToken ct = default);
    }
}