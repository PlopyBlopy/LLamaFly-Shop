using Core.Contracts.Dtos;
using FluentResults;

namespace Core.Interfaces
{
    public interface IProfileRepository
    {
        Task<Result<ProfileDto>> GetProfile(Guid id, CancellationToken ct);

        Task<Result> UpdateProfile(ProfileUpdateDto dto, CancellationToken ct);
    }
}