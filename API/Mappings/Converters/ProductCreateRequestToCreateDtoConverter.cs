using AutoMapper;
using Core.Contracts.Requests;
using Core.Contracts.Dtos;

namespace API.Mappings.Converters
{
    public class ProductCreateRequestToCreateDtoConverter : ITypeConverter<ProductCreateRequest, ProductCreateDto>
    {
        public ProductCreateDto Convert(ProductCreateRequest source, ProductCreateDto destination, ResolutionContext context)
        {
            return new ProductCreateDto(source.Title, source.Description, source.Price, source.CategoryId, source.SellerId);
        }
    }
}