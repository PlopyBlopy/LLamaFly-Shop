using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Profile : IProfileConstraints
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Profile(string name, string surname, string patronymic, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}