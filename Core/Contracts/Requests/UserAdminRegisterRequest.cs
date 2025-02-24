namespace Core.Contracts.Requests
{
    public record UserAdminRegisterRequest(UserRegisterRequest User, AdminRegisterRequest Admin);
}