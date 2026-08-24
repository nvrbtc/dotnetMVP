namespace dotnetMVP.Models.DTO.User
{
    public class JwtTokens
    {
        public string RefreshToken { get; init; } = string.Empty;
        public string AccessToken { get; init; } = string.Empty;
    }
}
