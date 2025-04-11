using Core.Contracts.Dtos;
using Core.Interfaces;
using Storage.Utilities;

namespace Storage.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStoragePathProvider _storageRoot;
        private readonly ImageFileReader _imageFileReader;

        public StorageService(IStoragePathProvider storageRoot, ImageFileReader imageFileReader)
        {
            _storageRoot = storageRoot;
            _imageFileReader = imageFileReader;
        }

        public async Task Save(ImageUploadDto dto, CancellationToken ct)
        {
            var folder = Path.Combine(
                _storageRoot.GetStorageRootPath(),
                dto.ProductId.ToString()
            );

            Directory.CreateDirectory(folder);

            foreach (var imageDto in dto.ImagesDtos)
            {
                var extension = imageDto.MimeType.Split('/').LastOrDefault() ?? "bin";
                var fileName = $"{imageDto.Order}.{extension}";
                var fullPath = Path.Combine(folder, fileName);

                await File.WriteAllBytesAsync(fullPath, imageDto.Data, ct);
            }
        }

        public async Task<ImageDto> ReadPreview(Guid productId, CancellationToken ct)
        {
            var folder = Path.Combine(
                _storageRoot.GetStorageRootPath(),
                productId.ToString());

            if (!Directory.Exists(folder))
            {
                //throw new DirectoryNotFoundException($"Folder for product {productId} not found");

                folder = Path.Combine(
                    _storageRoot.GetStorageRootPath(),
                    Guid.Empty.ToString());
            }

            var files = Directory.GetFiles(folder)
                .OrderBy(f => f)
                .ToList();

            if (!files.Any())
            {
                throw new FileNotFoundException($"No images found for product {productId}");
            }

            var firstFile = files.First();
            return await _imageFileReader.ReadImageFile(firstFile, ct);
        }

        public async Task<ImageDto> ReadImage(Guid productId, int order, CancellationToken ct)
        {
            //var folder = Path.Combine(
            //     _pathProvider.GetStorageRootPath(),
            //     "ImageFiles",
            //     "ProductImages",
            //     productId.ToString());

            var folder = Path.Combine(
                _storageRoot.GetStorageRootPath(),
                productId.ToString());

            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Folder for product {productId} not found");
            }

            var files = Directory.GetFiles(folder)
                .OrderBy(f => f)
                .ToList();

            if (!files.Any())
            {
                throw new FileNotFoundException($"No images found for product {productId}");
            }

            var firstFile = files[order];

            return await _imageFileReader.ReadImageFile(firstFile, ct);
        }

        public async Task<IEnumerable<ImageDto>> ReadAll(Guid productId, CancellationToken ct)
        {
            //var folder = Path.Combine(
            //    _pathProvider.GetStorageRootPath(),
            //    "ImageFiles",
            //    "ProductImages",
            //    productId.ToString());

            var folder = Path.Combine(
                _storageRoot.GetStorageRootPath(),
                productId.ToString());

            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Folder for product {productId} not found");
            }

            var files = Directory.GetFiles(folder)
                .OrderBy(f => f)
                .ToList();

            return await _imageFileReader.ReadImageFiles(files, ct);
        }

        public async Task Update(ImageUploadDto dto, CancellationToken ct)
        {
            // Формируем путь к папке продукта
            //var folder = Path.Combine(
            //    _pathProvider.GetStorageRootPath(),
            //    "ImageFiles",
            //    "ProductImages",
            //    dto.ProductId.ToString()
            //);

            //var folder = Path.Combine(
            //    _storageRoot.GetStorageRootPath(),
            //    productId.ToString());

            //// Проверяем существование папки
            //if (!Directory.Exists(folder))
            //{
            //    throw new DirectoryNotFoundException($"Folder for product {dto.ProductId} not found");
            //}

            //foreach (var imageDto in dto.ImagesDtos)
            //{
            //    // Генерируем шаблон имени файла для поиска
            //    var searchPattern = $"{imageDto.Order}.*";
            //    var existingFiles = Directory.GetFiles(folder, searchPattern);

            //    // Удаляем все файлы с таким Order
            //    foreach (var existingFile in existingFiles)
            //    {
            //        File.Delete(existingFile);
            //    }

            //    // Создаем новое имя файла
            //    var extension = imageDto.MimeType.Split('/').LastOrDefault() ?? "bin";
            //    var newFileName = $"{imageDto.Order}.{extension}";
            //    var fullPath = Path.Combine(folder, newFileName);

            //    // Асинхронно сохраняем новый файл
            //    await File.WriteAllBytesAsync(fullPath, imageDto.Data, ct);
            //}
        }
    }
}