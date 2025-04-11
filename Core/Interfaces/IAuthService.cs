using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using FluentResults;

namespace Core.Interfaces
{
    public interface IAuthService : IService
    {
        Task<Result<TokensDto>> Login(UserLoginRequest request, CancellationToken ct);

        Task<Result> RegisterAdmin(UserProfileAdminRegisterRequest request, CancellationToken ct);

        Task<Result> RegisterSeller(UserProfileSellerRegisterRequest request, CancellationToken ct);

        Task<Result> RegisterCustomer(UserProfileCustomerRegisterRequest request, CancellationToken ct);
    }
}