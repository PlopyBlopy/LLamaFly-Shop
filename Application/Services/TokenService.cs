using Core.Contracts.Dtos;
using Core.Interfaces;
using Core.Models;
using Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptionsMonitor<JwtOptions> _options;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(IOptionsMonitor<JwtOptions> optionsMonitor, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _options = optionsMonitor;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public string GenerateAccessToken(User model)
        {
            var secretKey = _options.CurrentValue.SecretKey
                ?? throw new ArgumentException("Jwt:SecretKey is not configured");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Id.ToString()),
                //new Claim(ClaimTypes.Role, model.Role),
                new Claim("role", model.Role.ToLower()), // Явно указываем короткий ключ
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.CurrentValue.Issuer,
                audience: _options.CurrentValue.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(User model)
        {
            //ALERT: время на сервере с базой данных = московское, в api под UtcNow = UTC
            return new RefreshToken(Guid.NewGuid(), model.Id, Guid.NewGuid().ToString(), DateTime.Now.AddDays(14), false, DateTime.Now);
        }

        public async Task Add(RefreshToken model, CancellationToken ct)
        {
            await _refreshTokenRepository.Add(model, ct);
        }

        public async Task<TokensDto> GetRefreshToken(RefreshTokenDto dto, CancellationToken ct)
        {
            var model = await _refreshTokenRepository.Get(dto, ct);

            //TODO: Validation если нету токена (model)
            //TODO: Validation если он отозван или срок действия истек
            //if (model == null)
            //    ...
            //if (model.IsRevoked)
            //    ...

            //ALERT: время на сервере с базой данных = московское, в api под UtcNow = UTC
            if (model.IsRevoked || model.Expires < DateTime.Now) throw new InvalidOperationException("token is old");

            var user = await _userRepository.GetById(model.UserId, ct);

            var accessToken = GenerateAccessToken(user.Value);
            RefreshToken refreshToken = GenerateRefreshToken(user.Value);

            await Revoke(dto, ct);
            await _refreshTokenRepository.Add(refreshToken, ct);

            return new TokensDto(accessToken, refreshToken);
        }

        public async Task Revoke(RefreshTokenDto dto, CancellationToken ct)
        {
            await _refreshTokenRepository.Revoke(dto, ct);
        }
    }
}