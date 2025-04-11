namespace Core.Contracts.Requests
{
    public record UserProfileAdminRegisterRequest(UserRegisterRequest User, ProfileRegisterRequest Profile, AdminRegisterRequest Admin);
}