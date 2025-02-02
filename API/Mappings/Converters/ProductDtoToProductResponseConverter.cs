using AutoMapper;
using Core.Contracts.Dto;
using API.Contracts.Responses;

namespace API.Mappings.Converters
{
    public class ProductDtoToProductResponseConverter : ITypeConverter<ProductDto, ProductResponse>
    {
        public ProductResponse Convert(ProductDto source, ProductResponse destination, ResolutionContext context)
        {
            return new ProductResponse(source.Id, source.Title, source.Description, source.Price, source.Rating, source.CreateAt, source.CategoryId, source.SellerId);
        }
    }
}