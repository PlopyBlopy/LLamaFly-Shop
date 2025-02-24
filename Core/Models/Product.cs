using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Product : ModelBase, IProductConstraints, IProductTitleConstraint, IProductDescriptionConstraint, IProductPriceConstraint, IProductRatingConstraint
    {
        public string Title { get; }

        public string Description { get; }
        public decimal Price { get; }
        public double Rating { get; }
        public DateTime CreatedAt { get; }
        public Guid CategoryId { get; }
        public Guid SellerId { get; }

        public Product(Guid id, string title, string description, decimal price, double rating, DateTime createAt, Guid categoryId, Guid sellerId) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            Rating = rating;
            CreatedAt = createAt;
            CategoryId = categoryId;
            SellerId = sellerId;
        }
    }
}