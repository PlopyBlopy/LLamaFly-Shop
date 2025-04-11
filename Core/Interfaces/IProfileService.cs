using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface IProfileService : IService
    {
        Task<Result<ProfileDto>> GetProfile(Guid id, CancellationToken ct);

        Task<Result> UpdateProfile(Guid id, ProfileUpdateRequest request, CancellationToken ct);
    }
}