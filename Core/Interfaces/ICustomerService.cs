using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface ICustomerService : IService, IServiceConsumer
    {
        Task<Result<CustomerDto>> GetCustomer(Guid id, CancellationToken ct);

        Task<Result> UpdateCustomer(Guid id, CustomerUpdateRequest request, CancellationToken ct);
    }
}