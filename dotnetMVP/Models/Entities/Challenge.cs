using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.Entities
{
    public class Challenge
    {
        public Guid Id { get; set; }
        // Foreign key to the Platform class + navigation property
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;
        //navigation property to the WriteUp class
        public List<WriteUp> WriteUps { get; set; } = new List<WriteUp>();
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        [MaxLength (50)]
        public string Title { get; set; } = string.Empty;
    }
}
