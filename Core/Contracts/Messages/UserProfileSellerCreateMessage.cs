namespace Core.Contracts.Messages
{
    public record UserProfileSellerCreateMessage(UserCreateMessage User, ProfileCreateMessage Profile, SellerCreateMessage Seller);
}