namespace Core.Contracts.Dtos
{
    public record UserProfileAdminCreateDto(UserCreateDto User, ProfileCreateDto Profile, AdminCreateDto Admin);
}