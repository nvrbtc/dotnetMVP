using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetMVP.Models.Entities
{
    public class WriteUp
    {
        // Primary key
        [Required]
        public Guid Id { get; set; }
        // Foreign key to the Challenge class + navigation property
        public Guid ChallengeId { get; set; }
        public Challenge Challenge { get; set; } = null!;
        // Foreign key to the AppUser class + navigation property
        public Guid AppUserId { get; set;}
        public AppUser AppUser { get; set; } = null!;
        //Entity properties
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string Text { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; //Maybe there is a better way to store hash of passwd 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }        
        public bool IsHidden { get; set; }
        public bool IsHiddenByAdmin { get; set; }
    }
}
