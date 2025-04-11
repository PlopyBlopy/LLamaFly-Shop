using AutoMapper;
using Core.Contracts.Requests;
using Core.Models;

namespace API.Mappings.Converters
{
    public class ProductCreateWithRatingRequestToModelConverter : ITypeConverter<ProductCreateWithRatingRequest, Product>
    {
        public Product Convert(ProductCreateWithRatingRequest source, Product destination, ResolutionContext context)
        {
            DateTime created = DateTime.Now;

            return new Product(Guid.NewGuid(), source.Title, source.Description, source.Price, source.Rating, source.CategoryId, source.SellerId, created, created);
        }
    }
}