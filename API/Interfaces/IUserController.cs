using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserController
    {
        Task<ActionResult<UserDto>> Get(CancellationToken ct);

        Task<IActionResult> Update(UserUpdateRequest request, CancellationToken ct);

        Task<IActionResult> Delete(CancellationToken ct);
    }
}