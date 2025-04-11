using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Product : ModelBase, IProductConstraints
    {
        public string Title { get; }
        public string Description { get; }
        public decimal Price { get; }
        public double Rating { get; }
        public Guid CategoryId { get; }
        public Guid SellerId { get; }
        public DateTime UpdatedAt { get; }
        public DateTime CreatedAt { get; }

        public Product(Guid id, string title, string description, decimal price, double rating, Guid categoryId, Guid sellerId, DateTime updatedAt, DateTime createAt) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            Rating = rating;
            CategoryId = categoryId;
            SellerId = sellerId;
            UpdatedAt = updatedAt;
            CreatedAt = createAt;
        }
    }
}