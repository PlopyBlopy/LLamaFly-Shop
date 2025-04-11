namespace Core.Contracts.Dtos
{
    public record ImageDto(int Order, string MimeType, byte[] Data);
}