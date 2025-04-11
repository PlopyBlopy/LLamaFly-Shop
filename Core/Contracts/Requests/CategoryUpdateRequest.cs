namespace Core.Contracts.Requests
{
    public record CategoryUpdateRequest(Guid Id, string Title, Guid ParentCategoryId);
}