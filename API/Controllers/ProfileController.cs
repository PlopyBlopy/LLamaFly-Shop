using API.Interfaces;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Controller]
    [Route("api/profiles")]
    public class ProfileController : ControllerBase, IProfileController
    {
        public ProfileController()
        {
        }

        public Task<ActionResult<ProfileDto>> Get(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(ProfileUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}