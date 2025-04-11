using Core.Contracts.Dtos;

namespace Storage.Utilities
{
    public class ImageFileReader
    {
        public async Task<ImageDto> ReadImageFile(string filePath, CancellationToken ct)
        {
            var fileInfo = new FileInfo(filePath);

            return new ImageDto(
                (int)char.GetNumericValue(fileInfo.Name[0]),
                GetMimeType(fileInfo.Extension),
                await File.ReadAllBytesAsync(filePath, ct));
        }

        public async Task<IEnumerable<ImageDto>> ReadImageFiles(List<string> filePaths, CancellationToken ct)
        {
            List<ImageDto> imageFiles = new List<ImageDto>();

            for (int i = 0; i < filePaths.Count; i++)
            {
                var fileInfo = new FileInfo(filePaths[i]);
                imageFiles.Add(new ImageDto(
                    (int)char.GetNumericValue(fileInfo.Name[0]),
                    GetMimeType(fileInfo.Extension),
                    await File.ReadAllBytesAsync(filePaths[i], ct)));
            }

            return imageFiles;
        }

        private string GetMimeType(string extension)
        {
            return extension.ToLowerInvariant() switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }
    }
}