using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IProfileController
    {
        Task<ActionResult<ProfileDto>> Get(CancellationToken ct);

        Task<IActionResult> Update(ProfileUpdateRequest request, CancellationToken ct);
    }
}