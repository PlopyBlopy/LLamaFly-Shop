using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Admin : IAdminConstraints
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Admin(DateTime createdAt, DateTime updatedAt)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}