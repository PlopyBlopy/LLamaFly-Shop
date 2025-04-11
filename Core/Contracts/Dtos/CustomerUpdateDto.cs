using Core.Contracts.Requests;
namespace Core.Contracts.Dtos
{
    public record CustomerUpdateDto(Guid id, DateTime UpdatedAt);
}
