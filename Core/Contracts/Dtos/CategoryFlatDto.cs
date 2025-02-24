namespace Core.Contracts.Dtos
{
    public record CategoryFlatDto(Guid Id, string Title, Guid? ParentCategoryId, int Level);
}
