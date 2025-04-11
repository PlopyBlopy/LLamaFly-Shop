namespace Core.Interfaces
{
    public interface IFileConverter
    {
        Task<byte[]> ConvertToWebP(Stream imageStream, int quality = 75, CancellationToken ct = default);
    }
}