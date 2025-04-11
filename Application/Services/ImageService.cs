using Core.Contracts.Dtos;
using Core.Interfaces;
using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IStorageService _storage;
        private readonly IDistributedCache _cache;
        private readonly IFileConverter _fileConverter;
        private const string image = "image-";
        private const string order = "order-";

        public ImageService(IStorageService storage, IDistributedCache cache, IFileConverter fileConverter)
        {
            _storage = storage;
            _cache = cache;
            _fileConverter = fileConverter;
        }

        public async Task Add(ImageUploadDto dto, CancellationToken ct)
        {
            await _storage.Save(dto, ct);
            await _cache.RemoveAsync("previews_zip");
        }

        public async Task<ImageDto> GetProductImageByOrder(Guid productId, int order, CancellationToken ct)
        {
            var result = await _storage.ReadImage(productId, order, ct);
            return result;
        }

        public async Task<byte[]> GetProductsPreviewImages(Guid[] productIds, bool supportsWebP, CancellationToken ct)
        {
            // Генерация хешированного ключа для Redis
            var hash = ComputeProductIdsHash(productIds);
            var cacheKey = $"previews_zip:{hash}:{(supportsWebP ? "webp" : "orig")}";

            return await _cache.GetAsync(cacheKey, async getMethod =>
            {
                var previews = new List<ImageProductDto>();
                var tasks = productIds.Select(async id =>
                {
                    var image = await _storage.ReadPreview(id, ct);
                    return new ImageProductDto(id, image);
                });

                previews.AddRange(await Task.WhenAll(tasks));

                using var memoryStream = new MemoryStream();
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var preview in previews)
                    {
                        var (imageData, extension) = await ProcessImage(preview.Image, supportsWebP, ct);
                        var entryName = $"{preview.ProductId}/preview.{extension}";

                        var entry = archive.CreateEntry(entryName);
                        using var entryStream = entry.Open();
                        await entryStream.WriteAsync(imageData, ct);
                    }
                }

                memoryStream.Position = 0;
                return memoryStream.ToArray();
                //return CompressData(memoryStream.ToArray());
            },
            options: null,
            ct);
        }

        private async Task<(byte[] Data, string Extension)> ProcessImage(ImageDto image, bool supportsWebP, CancellationToken ct)
        {
            try
            {
                if (!supportsWebP || image.MimeType == "image/webp")
                    return (image.Data, GetExtensionFromMimeType(image.MimeType));

                using var imageStream = new MemoryStream(image.Data);
                var converted = await _fileConverter.ConvertToWebP(imageStream, 75, ct);
                return (converted, "webp");
            }
            catch
            {
                return (image.Data, GetExtensionFromMimeType(image.MimeType));
            }
        }

        private byte[] CompressData(byte[] input)
        {
            //using var outputStream = new MemoryStream();
            //using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal))
            //{
            //    gzipStream.Write(input, 0, input.Length);
            //}
            //return outputStream.ToArray();
            return input;
        }

        private string ComputeProductIdsHash(Guid[] productIds)
        {
            var orderedIds = productIds.OrderBy(x => x).Select(x => x.ToString());
            var idsString = string.Join("|", orderedIds);

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(idsString));
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        private string GetExtensionFromMimeType(string mimeType)
        {
            return mimeType switch
            {
                "image/jpeg" => "jpg",
                "image/png" => "png",
                "image/gif" => "gif",
                "image/webp" => "webp",
                _ => "bin"
            };
        }

        //public async Task<IEnumerable<ImageProductDto>> GetProductsPreviewImages(Guid[] ids, CancellationToken ct)
        //{
        //    List<ImageProductDto> dtos = new List<ImageProductDto>();

        //    for (int i = 0; i < ids.Length; i++)
        //    {
        //        var image = await _storage.ReadPreview(ids[i], ct);
        //        ImageProductDto dto = new ImageProductDto(ids[i], image);
        //        dtos.Add(dto);
        //    }

        //    return dtos;
        //}

        public async Task<byte[]> GetProductImages(Guid productId, bool supportsWebP, CancellationToken ct)
        {
            var cacheKey = $"product_zip:{productId}:{(supportsWebP ? "webp" : "orig")}";

            return await _cache.GetAsync(cacheKey, async getMethod =>
            {
                var images = await _storage.ReadAll(productId, ct);

                if (!images.Any())
                    throw new InvalidOperationException("No images found for the product");

                using var memoryStream = new MemoryStream();
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var image in images)
                    {
                        var (imageData, extension) = await ProcessImage(image, supportsWebP, ct);

                        var entryName = $"{image.Order}.{extension}";
                        var entry = archive.CreateEntry(entryName);

                        using var entryStream = entry.Open();
                        await entryStream.WriteAsync(imageData, ct);
                    }
                }

                memoryStream.Position = 0;
                return memoryStream.ToArray();
                //return CompressData(memoryStream.ToArray());
            },
            options: null,
            ct);
        }

        //public async Task<IEnumerable<ImageDto>> GetProductImages(Guid productId, CancellationToken ct)
        //{
        //    var result = await _storage.ReadAll(productId, ct);

        //    return result;
        //}

        public async Task Update(ImageUploadDto dto, CancellationToken ct)
        {
            await _storage.Update(dto, ct);
        }

        public Task Delete(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<ImageDto> GetProductPreviewImage(Guid productId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageDto>> GetAllProductImages(Guid productId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}