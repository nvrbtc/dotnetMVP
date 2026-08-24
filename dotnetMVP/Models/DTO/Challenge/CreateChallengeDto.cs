using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.ChallengeDto
{
    public record CreateChallengeDto
    {
        [Required(ErrorMessage = "PlatformId is required")]
        public Guid PlatformId { get; init; }

        [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; init; } = string.Empty;

        [MaxLength(200,ErrorMessage = "Description cannot be longer than 200 characters")]
        public string Description { get; init; } = string.Empty;

        [MaxLength(2000,ErrorMessage = "Text cannot be longer than 2000 characters")]
        public string Text { get; init;  } = string.Empty;
    }
}
