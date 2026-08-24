using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.User
{
    public record CreateUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(15, ErrorMessage = "Username cannot exceed 15 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_.]+$", 
            ErrorMessage = "Username can only contain letters, numbers, periods, and underscores.")]
        public string Username { get; init; } = string.Empty;


        [Required(ErrorMessage = "Password is required.")]
        //Password policy is defined by Identity in program.cs
        public string Password { get; init; } = string.Empty;


        [Required(ErrorMessage = "Confirmation is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; init; } = string.Empty;

    }
}
