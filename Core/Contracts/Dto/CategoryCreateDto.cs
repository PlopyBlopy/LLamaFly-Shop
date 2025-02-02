namespace Core.Contracts.Dto
{
    public record CategoryCreateDto(string Title, Guid ParentCategoryId);
}