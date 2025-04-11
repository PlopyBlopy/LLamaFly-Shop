using API.Interfaces;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [ApiController]
    [Route("api/profiles/sellers")]
    public class SellerController : ControllerBase, ISellerController
    {
        private readonly ISellerService _service;

        public SellerController(ISellerService service)
        {
            _service = service;
        }

        //public Task<ActionResult<SellerDto>> Get(CancellationToken ct)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<IActionResult> Update(SellerUpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        [HttpGet("isexist")]
        public async Task<ActionResult<bool>> IsExist([FromQuery] Guid id, CancellationToken ct)
        {
            var result = await _service.IsExist(id, ct);

            return Ok(result);
        }

        //[Authorize(Policy = "admin")]
        //[HttpPost]
        //public async Task AddProfile(UserSellerProfileDto request, CancellationToken ct)
        //{
        //    await _service.AddConsumerProfileMessage(request, ct);
        //}

        [Authorize(Policy = "seller")]
        [HttpGet]
        public async Task<ActionResult<SellerDto>> GetSeller(CancellationToken ct)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                var handler = new JwtSecurityTokenHandler();

                var jsonToken = handler.ReadJwtToken(token);
                var sub = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                var profileId = Guid.Parse(sub);

                var result = await _service.GetSeller(profileId, ct);

                return Ok(result.Value);
            }

            return BadRequest("Invalid token");
        }

        ////[Authorize(Policy = "admin")]
        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<SellerDto>>> GetProfiles(CancellationToken ct)
        //{
        //    var result = await _service.GetProfiles(ct);

        //    if (result is null)
        //        return NotFound("There are no profiles.");

        //    return Ok(result);
        //}
    }
}