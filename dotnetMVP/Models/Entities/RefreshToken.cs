using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.Entities
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public string? RefreshSecret { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        //Navigational properties
        [Required]
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;
    }
}
