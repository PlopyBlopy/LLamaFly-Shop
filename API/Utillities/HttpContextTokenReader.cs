using FluentResults;
using System.IdentityModel.Tokens.Jwt;

namespace API.Utillities
{
    public interface IHttpContextTokenReader
    {
        Task<Result<Guid>> SubReared(string authHeader);
    }

    public class HttpContextTokenReader : IHttpContextTokenReader
    {
        public async Task<Result<Guid>> SubReared(string authHeader)
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadJwtToken(token);
            var sub = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            Guid? profileId = Guid.Parse(sub);

            if (profileId == null || profileId.Value == Guid.Empty)
            {
                return Result.Fail("Profile id is null or empty");
            }

            return Result.Ok(profileId.Value);
        }
    }
}