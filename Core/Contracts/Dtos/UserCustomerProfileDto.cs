namespace Core.Contracts.Dtos
{
    public record UserCustomerProfileDto(UserProfileDto User, CustomerProfileDto Customer);
}