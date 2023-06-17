using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Interfaces
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GenerateToken(IEnumerable<Claim> claims);
    }
}
