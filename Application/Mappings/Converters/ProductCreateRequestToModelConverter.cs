using AutoMapper;
using Core.Contracts.Requests;
using Core.Models;

namespace API.Mappings.Converters
{
    public class ProductCreateRequestToModelConverter : ITypeConverter<ProductCreateRequest, Product>
    {
        public Product Convert(ProductCreateRequest source, Product destination, ResolutionContext context)
        {
            DateTime created = DateTime.Now;

            return new Product(Guid.NewGuid(), source.Title, source.Description, source.Price, 0.0, source.CategoryId, source.SellerId, created, created);
        }
    }
}