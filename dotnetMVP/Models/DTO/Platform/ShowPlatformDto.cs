using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.PlatformDto
{
    public record ShowPlatformDto
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; init; }

        [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        public string Name { get; init; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; init; } = string.Empty;

    }
}
