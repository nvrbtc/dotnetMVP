using dotnetMVP.Models.DTO.User;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace dotnetMVP.Services.Realization
{
    public class JwtHandler : IJwtHandler
    {
        private  JwtOptions _jwtOptions;
        public JwtHandler(IOptions<JwtOptions> op)
        {
         _jwtOptions = op.Value;   
        }
        public string GenerateJwtToken(UserClaims dto)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, dto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in dto.Claims ?? new List<string>())
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signing = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SymmetricKey)),
                                                    SecurityAlgorithms.HmacSha256Signature);


            var tokenD = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _jwtOptions.ValidIssuer,
                Audience = _jwtOptions.ValidAudience,
                SigningCredentials = signing
            };
            var token = handler.CreateToken(tokenD);
            return handler.WriteToken(token);

        }

        public string GenerateRefreshToken()
        {
            byte[] value = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(value);
            }
            return Convert.ToBase64String(value);

        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var parameters = new TokenValidationParameters()
            {
                ValidateLifetime = _jwtOptions.ValidateLifetime,
                ValidAlgorithms = _jwtOptions.ValidAlgotrithms,
                ValidIssuer = _jwtOptions.ValidIssuer,
                ValidateIssuer = _jwtOptions.ValidateIssuer,
                ValidAudience = _jwtOptions.ValidAudience,

                ValidateIssuerSigningKey = _jwtOptions.ValidateKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SymmetricKey)),

                
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, parameters, out SecurityToken validated);
        }

    }
}
