namespace Core.Contracts.Dtos
{
    public record UserProfileCustomerCreateDto(UserCreateDto User, ProfileCreateDto Profile, CustomerCreateDto Customer);
}