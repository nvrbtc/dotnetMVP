using System.Security.Claims;

namespace dotnetMVP.Models.DTO.User
{
    public record UserClaims
    {
        public Guid Id { get; init; }
        public IList<string>? Claims { get; init; }
    }
}
