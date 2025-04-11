using API.Interfaces;
using AutoMapper;
using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IErrorFactoryHandler _errorFactoryHandler;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService service, IMapper mapper, ITokenService tokenService, IErrorFactoryHandler errorFactoryHandler)
        {
            _tokenService = tokenService;
            _errorFactoryHandler = errorFactoryHandler;
            _authService = service;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request, CancellationToken ct)
        {
            var newTokens = await _authService.Login(request, ct);

            Response.Cookies.Append("refreshToken", newTokens.Value.RefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newTokens.Value.RefreshToken.Expires,
            });

            return Ok(new
            {
                AccessToken = newTokens.Value.AccessToken,
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> logout(CancellationToken ct)
        {
            var refreshTokenValue = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshTokenValue))
                return BadRequest("Refresh token not found");

            var dto = new RefreshTokenDto(refreshTokenValue);

            await _tokenService.Revoke(dto, ct);

            return Ok();
        }

        //ALERT: Дублирование кода для RegisterAdmin, RegisterSeller, RegisterCustomer
        [Authorize(Policy = "admin")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserProfileAdminRegisterRequest request, CancellationToken ct)
        {
            var result = await _authService.RegisterAdmin(request, ct);

            return result.IsSuccess
            ? Ok()
            : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [HttpPost("register/seller")]
        public async Task<IActionResult> RegisterSeller([FromBody] UserProfileSellerRegisterRequest request, CancellationToken ct)
        {
            var result = await _authService.RegisterSeller(request, ct);

            return result.IsSuccess
            ? Ok()
            : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }

        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserProfileCustomerRegisterRequest request, CancellationToken ct)
        {
            var result = await _authService.RegisterCustomer(request, ct);

            return result.IsSuccess
                ? Ok()
                : _errorFactoryHandler.GetHandler(result.Errors.First()).Handle(result.Errors.First());
        }
    }
}