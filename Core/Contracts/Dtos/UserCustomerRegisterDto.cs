namespace Core.Contracts.Dtos
{
    public record UserCustomerRegisterDto(UserRegisterDto User, CustomerRegisterDto Customer);
}