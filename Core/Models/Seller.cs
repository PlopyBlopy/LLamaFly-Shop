using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Seller : ISellerConstraints
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Seller(DateTime createdAt, DateTime updatedAt)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}