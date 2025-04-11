namespace Core.Contracts.Messages
{
    public record ProfileCreateMessage(Guid Id, string Name, string Surname, string Patronymic, DateTime CreatedAt, DateTime UpdatedAt);
}