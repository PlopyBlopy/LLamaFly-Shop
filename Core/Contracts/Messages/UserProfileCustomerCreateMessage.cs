namespace Core.Contracts.Messages
{
    public record UserProfileCustomerCreateMessage(UserCreateMessage User, ProfileCreateMessage Profile, CustomerCreateMessage Customer);
}