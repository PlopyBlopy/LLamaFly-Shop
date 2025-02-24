namespace Core.Contracts.Requests
{
    public record CategoryCreateRequest(string Title, Guid? ParentCategoryId);
}
