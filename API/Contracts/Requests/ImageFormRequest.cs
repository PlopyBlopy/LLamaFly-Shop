namespace API.Contracts.Requests
{
    public record ImageFormRequest(int Order, IFormFile Image);
}