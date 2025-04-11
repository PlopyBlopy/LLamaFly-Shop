using AutoMapper;
using Core.Contracts.Dtos;
using API.Contracts.Requests;

namespace API.Mapping.Converters
{
    public class ImageCreateRequestToCreateDtoConverter : ITypeConverter<ImageUploadRequest, ImageUploadDto>
    {
        public ImageUploadDto Convert(ImageUploadRequest source, ImageUploadDto destination, ResolutionContext context)
        {
            var dtos = source.Images.Select(i => new ImageDto(
                //Guid.NewGuid(),
                i.Order,
                i.Image.ContentType,
                context.Mapper.Map<byte[]>(i.Image) // Используем зарегистрированный маппинг
            )).ToList();

            return new ImageUploadDto(source.ProductId, dtos);
        }
    }
}