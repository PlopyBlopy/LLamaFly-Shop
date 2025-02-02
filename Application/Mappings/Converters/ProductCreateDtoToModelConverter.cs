using AutoMapper;
using Core.Contracts.Dto;
using Core.Models;

namespace Application.Mappings.Converters
{
    public class ProductCreateDtoToModelConverter : ITypeConverter<ProductCreateDto, Product>
    {
        public Product Convert(ProductCreateDto source, Product destination, ResolutionContext context)
        {
            return new Product(Guid.NewGuid(), source.Title, source.Description, source.Price, source.Rating, DateTime.UtcNow, source.CategoryId, source.SellerId);
        }
    }
}