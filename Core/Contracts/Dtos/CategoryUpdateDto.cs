namespace Core.Contracts.Requests
{
    public record CategoryUpdateDto(Guid Id, string? Title, Guid? ParentCategoryId);
}