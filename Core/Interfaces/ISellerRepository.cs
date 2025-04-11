using Core.Contracts.Dtos;
using FluentResults;

namespace Core.Interfaces
{
    public interface ISellerRepository : IRepository
    {
        Task<Result<Guid>> AddConsumerSeller(UserProfileSellerCreateDto dto, CancellationToken ct);

        Task<Result<SellerDto>> GetSeller(Guid id, CancellationToken ct);

        Task<Result> UpdateSeller(SellerUpdateDto dto, CancellationToken ct);

        Task<Result<bool>> IsExist(Guid id, CancellationToken ct);
    }
}