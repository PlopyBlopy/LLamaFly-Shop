namespace Core.Contracts.Requests
{
    public record UserCustomerRegisterRequest(UserRegisterRequest User, CustomerRegisterRequest Customer);
}