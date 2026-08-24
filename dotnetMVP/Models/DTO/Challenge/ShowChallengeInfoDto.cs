using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.ChallengeDto
{
    public record ShowChallengeInfoDto
    {
        [Required(ErrorMessage = "ChallengeId is required.")]

        public Guid ChallengeId { get; init; }
        [Required(ErrorMessage = "PlatformId is required.")]
        public Guid PlatformId { get; init; }

        [MaxLength(50,ErrorMessage = "PlatformName cannot exceed 50 characters.")]
        public string PlatformName { get; init; } = string.Empty;

        [MaxLength(50, ErrorMessage = "ChallengeName cannot exceed 50 characters.")]
        public string ChallengeName { get; init; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; init; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; init; } = string.Empty;
    }
}
