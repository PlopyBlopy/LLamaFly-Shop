using Core.Interfaces.Constraints;

namespace Core.Models
{
    public abstract class User : IUserConstraints
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User(Guid id, string role, string login, string email, string? phoneNumber, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Role = role;
            Login = login;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}