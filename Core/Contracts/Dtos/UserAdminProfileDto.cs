namespace Core.Contracts.Dtos
{
    public record UserAdminProfileDto(UserProfileDto User, AdminProfileDto Customer);
}