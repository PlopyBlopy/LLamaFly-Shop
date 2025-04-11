using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAdminController
    {
        Task<ActionResult<AdminDto>> Get(CancellationToken ct);

        Task<IActionResult> Update(AdminUpdateRequest request, CancellationToken ct);
    }
}