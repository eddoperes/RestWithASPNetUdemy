using System.Security.Claims;

namespace RestWithASPNetUdemy.Service
{
    public interface ITokenService
    {

        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
