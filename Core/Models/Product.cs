using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Product : IProductConstraints, IProductTitleConstraint, IProductDescriptionConstraint, IProductPriceConstraint, IProductRatingConstraint
    {
        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; }
        public double Rating { get; }
        public DateTime CreateAt { get; }
        public Guid CategoryId { get; }
        public Guid SellerId { get; }

        public Product(Guid id, string title, string description, decimal price, double rating, DateTime createAt, Guid categoryId, Guid sellerId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Rating = rating;
            CreateAt = createAt;
            CategoryId = categoryId;
            SellerId = sellerId;
        }
    }
}