namespace Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        //public string UpdatedAt { get; set; }
        //public string CreatedAt { get; set; }

        public User(Guid id, string role, string login, string email, string? phoneNumber, string password)
        {
            Id = id;
            Role = role;
            Login = login;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }
    }
}