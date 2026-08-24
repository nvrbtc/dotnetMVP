using dotnetMVP.Models.DTO.User;
using System.Security.Claims;

namespace dotnetMVP.Services.Interface
{
    public interface IJwtHandler
    {
        string GenerateJwtToken(UserClaims dto);
        string GenerateRefreshToken();
        //Task<string> RefreshTokenAsync(string refreshToken);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
