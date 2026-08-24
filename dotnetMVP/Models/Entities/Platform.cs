using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.Entities
{
    public class Platform
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        //navigation property to the Challenge class
        public List<Challenge> AvailableChallenges { get; set; } = new List<Challenge>();

    }
}
