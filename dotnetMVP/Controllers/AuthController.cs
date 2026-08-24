using dotnetMVP.Models.DTO.User;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace dotnetMVP.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService auth)
        {
            _authService = auth;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginAttempDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return this.ResultToHttpCode(result);
        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] JwtTokens jwtTokens)
        {
            using (LogContext.PushProperty("AuthTokens", "Refresh"))
            {
                var res = await _authService.RefreshAsync(jwtTokens);
                return this.ResultToHttpCode(res);
            }
        }
    }
}
