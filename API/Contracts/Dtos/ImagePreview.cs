using Microsoft.AspNetCore.Mvc;

namespace API.Contracts.Dtos
{
    public record ImagePreview(Guid ProductId, FileContentResult Image);
}