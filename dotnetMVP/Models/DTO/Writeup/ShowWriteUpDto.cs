using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.Writeup
{
    public record ShowWriteUpDto
    {
        [Required]
        public Guid Id { get; init; }
        [MaxLength(50)]
        public string Challenge { get; init; } = string.Empty; // From options in web
        [MaxLength(50,ErrorMessage = "Max length is 50.")]
        public string Title { get; init; } = string.Empty;
        [MaxLength(2000,ErrorMessage = "Max length is 2000.")]
        public string Text { get; init; } = string.Empty;
    }
}
