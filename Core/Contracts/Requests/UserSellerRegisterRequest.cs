namespace Core.Contracts.Requests
{
    public record UserSellerRegisterRequest(UserRegisterRequest User, SellerRegisterRequest Seller);
}