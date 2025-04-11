using Core.Contracts.Dtos;
using FluentResults;

namespace Core.Interfaces
{
    public interface IAdminRepository : IRepository
    {
        Task<Result<Guid>> AddConsumerAdmin(UserProfileAdminCreateDto dto, CancellationToken ct);

        Task<Result<AdminDto>> GetAdmin(Guid id, CancellationToken ct);

        Task<Result> UpdateAdmin(AdminUpdateDto dto, CancellationToken ct);
    }
}