using Core.Contracts.Dtos;

namespace Core.Contracts.Dtos
{
    public record UserSellerRegisterDto(UserRegisterDto User, SellerRegisterDto Seller);
}
