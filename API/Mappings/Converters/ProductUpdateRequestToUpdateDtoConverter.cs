using AutoMapper;
using Core.Contracts.Requests;
using Core.Contracts.Dtos;

namespace API.Mappings.Converters
{
    public class ProductUpdateRequestToUpdateDtoConverter : ITypeConverter<ProductUpdateRequest, ProductUpdateDto>
    {
        public ProductUpdateDto Convert(ProductUpdateRequest source, ProductUpdateDto destination, ResolutionContext context)
        {
            return new ProductUpdateDto(source.Id, source?.Title, source?.Description, source?.Price, source?.CategoryId);
        }
    }
}