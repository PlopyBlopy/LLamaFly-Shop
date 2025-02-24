using Core.Contracts.Dtos;

namespace Core.Contracts.Dtos
{
    public record UserSellerProfileDto(UserProfileDto User, SellerProfileDto Seller);
}
