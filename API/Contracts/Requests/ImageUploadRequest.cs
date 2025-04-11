
namespace API.Contracts.Requests
{
    public record ImageUploadRequest(Guid ProductId, List<ImageFormRequest> Images);
}
