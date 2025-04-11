using API.Contracts.Requests;
using API.Mapping.Converters;
using API.Mapping.Resolvers;
using AutoMapper;
using Core.Contracts.Dtos;

namespace API.Mapping.Profiles
{
    public class ImageApiProfile : Profile
    {
        public ImageApiProfile()
        {
            CreateMap<IFormFile, byte[]>()
              .ConvertUsing<FormFileToByteArrayConverter>();

            #region Converters

            CreateMap<ImageUploadRequest, ImageUploadDto>().ConvertUsing<ImageCreateRequestToCreateDtoConverter>();

            #endregion Converters

            //#region Resolves

            //// Настройка маппинга для свойства ImageRequest -> byte[] с использованием резолвера
            //CreateMap<ImageFormRequest, ImageDto>()
            //    .ForMember(dest => dest.Data, opt => opt.MapFrom<ByteArrayResolver>());

            //#endregion Resolves
        }
    }

    public class FormFileToByteArrayConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(
            IFormFile source,
            byte[] destination,
            ResolutionContext context)
        {
            if (source == null || source.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            source.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}