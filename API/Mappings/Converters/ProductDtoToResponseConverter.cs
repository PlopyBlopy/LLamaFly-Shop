using AutoMapper;
using Core.Contracts.Responses;
using Core.Contracts.Dtos;

namespace API.Mappings.Converters
{
    public class ProductDtoToResponseConverter : ITypeConverter<ProductDto, ProductResponse>
    {
        public ProductResponse Convert(ProductDto source, ProductResponse destination, ResolutionContext context)
        {
            return new ProductResponse(source.Id, source.Title, source.Description, source.Price, source.Rating);
        }
    }
}