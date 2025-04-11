namespace Core.Contracts.Dtos
{
    public record CategoryDto(Guid Id, string Title, Guid? ParentCategoryId);
}