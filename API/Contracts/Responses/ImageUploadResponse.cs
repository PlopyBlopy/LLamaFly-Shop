using API.Contracts.Requests;

namespace API.Contracts.Responses
{
    public record ImageUploadResponse(Guid ProductId, List<ImageFormRequest> Images);
}