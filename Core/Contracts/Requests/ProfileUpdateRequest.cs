namespace Core.Contracts.Requests
{
    public record ProfileUpdateRequest(string? Name, string? Surname, string? Patronymic);
}