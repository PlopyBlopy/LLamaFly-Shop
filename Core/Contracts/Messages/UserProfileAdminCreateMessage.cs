namespace Core.Contracts.Messages
{
    public record UserProfileAdminCreateMessage(UserCreateMessage User, ProfileCreateMessage Profile, AdminCreateMessage Admin);
}