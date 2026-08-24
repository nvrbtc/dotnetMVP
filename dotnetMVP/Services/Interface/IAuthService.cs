using dotnetMVP.Models.DTO.User;
using dotnetMVP.Types;

namespace dotnetMVP.Services.Interface
{
    public interface IAuthService
    {
        Task<ServiceResult<JwtTokens>> LoginAsync(LoginAttempDto loginAttempDto);
        Task<ServiceResult<JwtTokens>> RefreshAsync(JwtTokens tokens);
    }
}
