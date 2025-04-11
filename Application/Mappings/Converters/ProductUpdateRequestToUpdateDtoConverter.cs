using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;

namespace API.Mappings.Converters
{
    public class ProductUpdateRequestToUpdateDtoConverter : ITypeConverter<ProductUpdateRequest, ProductUpdateDto>
    {
        public ProductUpdateDto Convert(ProductUpdateRequest source, ProductUpdateDto destination, ResolutionContext context)
        {
            DateTime updated = DateTime.Now;

            return new ProductUpdateDto(source.Id, source.Title, source.Description, source.Price, source.CategoryId, updated);
        }
    }
}