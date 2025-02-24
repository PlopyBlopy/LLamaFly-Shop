namespace Core.Contracts.Dtos
{
    public record CategoryCreateDto(string Title, Guid? ParentCategoryId);
}
