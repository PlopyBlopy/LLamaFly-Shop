using Core.Contracts.Dtos;

namespace Core.Interfaces
{
    public interface IImageService //: IService<ImageDto, ImageUploadDto>
    {
        public Task Add(ImageUploadDto dto, CancellationToken ct);

        public Task<ImageDto> GetProductPreviewImage(Guid productId, CancellationToken ct);

        //public Task<IEnumerable<ImageProductDto>> GetProductsPreviewImages(Guid[] ids, CancellationToken ct);

        public Task<byte[]> GetProductsPreviewImages(Guid[] productIds, bool supportsWebP, CancellationToken ct);

        public Task<byte[]> GetProductImages(Guid productId, bool supportsWebP, CancellationToken ct);

        public Task<ImageDto> GetProductImageByOrder(Guid productId, int order, CancellationToken ct);

        public Task<IEnumerable<ImageDto>> GetAllProductImages(Guid productId, CancellationToken ct);

        public Task Update(ImageUploadDto dto, CancellationToken ct);

        public Task Delete(Guid id, CancellationToken ct);
    }
}