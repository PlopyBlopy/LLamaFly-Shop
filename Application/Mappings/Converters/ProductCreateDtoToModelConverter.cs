using AutoMapper;
using Core.Models;
using Core.Contracts.Dtos;

namespace Application.Mappings.Converters
{
    public class ProductCreateDtoToModelConverter : ITypeConverter<ProductCreateDto, Product>
    {
        public Product Convert(ProductCreateDto source, Product destination, ResolutionContext context)
        {
            return new Product(Guid.NewGuid(), source.Title, source.Description, source.Price, 0.0, DateTime.UtcNow, source.CategoryId, source.SellerId);
        }
    }
}