namespace Core.Contracts.Requests
{
    public record UserProfileCustomerRegisterRequest(UserRegisterRequest User, ProfileRegisterRequest Profile, CustomerRegisterRequest Customer);
}