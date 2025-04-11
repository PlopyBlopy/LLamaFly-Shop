namespace Core.Contracts.Dtos
{
    public record UserProfileSellerCreateDto(UserCreateDto User, ProfileCreateDto Profile, SellerCreateDto Seller);
}