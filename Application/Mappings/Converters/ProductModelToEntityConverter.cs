using AutoMapper;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Models;

namespace Application.Mappings.Converters
{
    public class ProductModelToEntityConverter : ITypeConverter<Product, ProductEntity>
    {
        public ProductEntity Convert(Product src, ProductEntity dest, ResolutionContext context)
        {
            var entity = new ProductEntity()
            {
                Id = src.Id,
                Title = src.Title,
                Description = src.Description,
                Price = src.Price,
                Rating = src.Rating,
                CreateAt = src.CreateAt,
                CategoryId = src.CategoryId,
                SellerId = src.SellerId,
            };
            return entity;
        }
    }
}