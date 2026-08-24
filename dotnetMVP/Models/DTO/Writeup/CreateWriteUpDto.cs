using System.ComponentModel.DataAnnotations;

namespace dotnetMVP.Models.DTO.Writeup
{
    public record CreateWriteUpDto
    {
        [Required(ErrorMessage = "Challenge Id is required.")]
        public Guid ChallengeId { get;  init; }

        //[Required(ErrorMessage = "User id is reqiured")]   #testing purpouse
        //public Guid AppUserId { get;  init; }
        [MaxLength(50,ErrorMessage = "Max length is 50 chars.")]
        public string Title { get;  init; } = string.Empty;

        [MaxLength(2000,ErrorMessage = "Max length is 2000 chars.")]
        public string Text  { get;  init; } = string.Empty;
    }
}
