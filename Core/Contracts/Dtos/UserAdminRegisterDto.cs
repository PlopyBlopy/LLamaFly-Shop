namespace Core.Contracts.Dtos
{
    public record UserAdminRegisterDto(UserRegisterDto User, AdminRegisterDto Admin);
}