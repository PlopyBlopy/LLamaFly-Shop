using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Customer : ICustomerConstraints
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Customer(DateTime createdAt, DateTime updatedAt)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}