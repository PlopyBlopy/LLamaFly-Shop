using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface IStorageService
    {
        Task Save(ImageUploadDto dto, CancellationToken ct);

        Task<ImageDto> ReadPreview(Guid productId, CancellationToken ct);

        Task<ImageDto> ReadImage(Guid productId, int order, CancellationToken ct);

        Task<IEnumerable<ImageDto>> ReadAll(Guid productId, CancellationToken ct);

        Task Update(ImageUploadDto dto, CancellationToken ct);
    }
}