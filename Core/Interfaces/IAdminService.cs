using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface IAdminService : IService, IServiceConsumer
    {
        Task<Result<AdminDto>> GetAdmin(Guid id, CancellationToken ct);

        Task<Result> UpdateAdmin(Guid id, AdminUpdateRequest request, CancellationToken ct);
    }
}