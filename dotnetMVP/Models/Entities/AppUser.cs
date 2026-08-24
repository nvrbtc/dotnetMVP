using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        [MaxLength(200)]
        public string Bio { get; set; } = string.Empty;
        //navigation property to the WriteUp class
        public List<WriteUp> WriteUps { get; set; } = new List<WriteUp>();
        //nav.prop. to RefreshTokens
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
