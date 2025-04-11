using API.Interfaces;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/profiles/users")]
    public class UserController : ControllerBase, IUserController
    {
        public UserController()
        {
        }

        public Task<ActionResult<UserDto>> Get(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(UserUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}