using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface ISellerService : IService, IServiceConsumer
    {
        Task<Result<SellerDto>> GetSeller(Guid id, CancellationToken ct);

        Task<Result> UpdateSeller(Guid id, SellerUpdateRequest request, CancellationToken ct);

        Task<Result<bool>> IsExist(Guid id, CancellationToken ct);
    }
}