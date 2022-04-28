using System.Security.Claims;

namespace RestWithASPNetUdemy.Service
{
    public interface ITokenService
    {

        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken(IEnumerable<Claim> claims);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
