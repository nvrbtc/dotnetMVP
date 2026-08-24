using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.PlatformDto
{
    public record CreatePllatformDto
    {
        
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(30,ErrorMessage = "Name cannot exceed 30 characters.")]
        public string Name { get; init; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(200,ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; init; } = string.Empty;
    }
}
