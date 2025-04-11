namespace Core.Contracts.Dtos
{
    public record ImageUploadDto(Guid ProductId, List<ImageDto> ImagesDtos);
}