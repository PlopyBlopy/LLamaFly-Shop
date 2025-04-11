using API.Contracts.Dtos;

namespace API.Contracts.Responses
{
    public record ImagesPreviewsResponse(IEnumerable<ImagePreview> images);
}