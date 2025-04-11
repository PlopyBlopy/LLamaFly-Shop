using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ISellerController
    {
        Task<ActionResult<SellerDto>> GetSeller(CancellationToken ct);

        Task<IActionResult> Update(SellerUpdateRequest request, CancellationToken ct);

        Task<ActionResult<bool>> IsExist([FromQuery] Guid id, CancellationToken ct);
    }
}