using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth/token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _service;
        private readonly IMapper _mapper;

        public TokenController(ITokenService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(CancellationToken ct)
        {
            var refreshTokenValue = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshTokenValue))
                return BadRequest("Refresh token not found");

            var dto = _mapper.Map<RefreshTokenDto>(refreshTokenValue);
            var newTokens = await _service.GetRefreshToken(dto, ct);

            Response.Cookies.Append("refreshToken", newTokens.RefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newTokens.RefreshToken.Expires
            });

            return Ok(new
            {
                AccessToken = newTokens.AccessToken,
            });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest request, CancellationToken ct)
        {
            var dto = _mapper.Map<RefreshTokenDto>(request);

            await _service.Revoke(dto, ct);

            return Ok();
        }
    }
}