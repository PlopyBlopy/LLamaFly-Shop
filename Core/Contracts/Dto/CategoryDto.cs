namespace Core.Contracts.Dto
{
    public record CategoryDto(Guid Id, string Title, Guid ParentCategoryId, ICollection<CategoryDto> Subcategories);
}