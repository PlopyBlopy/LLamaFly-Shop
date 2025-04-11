namespace Core.Contracts.Requests
{
    public record UserProfileSellerRegisterRequest(UserRegisterRequest User, ProfileRegisterRequest Profile, SellerRegisterRequest Seller);
}