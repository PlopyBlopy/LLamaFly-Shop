using Core.Contracts.Dtos;
using FluentResults;

namespace Core.Interfaces
{
    public interface ICustomerRepository : IRepository
    {
        Task<Result<Guid>> AddConsumerCustomer(UserProfileCustomerCreateDto dto, CancellationToken ct);

        Task<Result<CustomerDto>> GetCustomer(Guid id, CancellationToken ct);

        Task<Result> UpdateCustomer(CustomerUpdateDto dto, CancellationToken ct);
    }
}