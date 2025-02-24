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
    public class JwtTokenService : ITokenService
    {
        private readonly IOptionsMonitor<JwtOptions> _options;

        public JwtTokenService(IOptionsMonitor<JwtOptions> optionsMonitor)
        {
            _options = optionsMonitor;
        }

        public string GenerateToken(User user)
        {
            var secretKey = _options.CurrentValue.SecretKey
                ?? throw new ArgumentException("Jwt:SecretKey is not configured");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _options.CurrentValue.Issuer,
                audience: _options.CurrentValue.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}