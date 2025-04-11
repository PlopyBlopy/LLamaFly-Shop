using Core.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace API.Utilities
{
    public class FileConverter : IFileConverter
    {
        public async Task<byte[]> ConvertToWebP(Stream imageStream, int quality = 75, CancellationToken ct = default)
        {
            using var image = await Image.LoadAsync(imageStream, ct);
            var encode = new WebpEncoder { Quality = quality };

            using var ms = new MemoryStream();
            await image.SaveAsync(ms, encode, ct);
            return ms.ToArray();
        }
    }
}