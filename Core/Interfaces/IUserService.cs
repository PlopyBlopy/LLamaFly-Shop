using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface IUserService : IService
    {
        Task<string> Login(UserLoginDto dto, CancellationToken ct);

        Task RegisterAdmin(UserAdminRegisterDto dto, CancellationToken ct);

        Task RegisterSeller(UserSellerRegisterDto dto, CancellationToken ct);

        Task RegisterCustomer(UserCustomerRegisterDto dto, CancellationToken ct);
    }
}