using AutoMapper;
using Core.Contracts.Responses;
using Core.Contracts.Dtos;

namespace API.Mappings.Converters
{
    public class ProductSellerDtoToSellerResponseConverter : ITypeConverter<ProductSellerDto, ProductSellerResponse>
    {
        public ProductSellerResponse Convert(ProductSellerDto source, ProductSellerResponse destination, ResolutionContext context)
        {
            return new ProductSellerResponse(source.Id, source.Title, source.Description, source.Price, source.CreatedAt, source.CategoryId);
        }
    }
}