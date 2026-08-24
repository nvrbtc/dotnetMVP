namespace dotnetMVP.Models.Entities
{
    // Not implemented yet, but this class represents a comment made by a user on a challenge.
    public class Comment
    {
        public Guid Id { get; set; }
        //navigation property to the AppUser class
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        //navigation property to the Challenge class
        public Guid ChallengeId { get; set; }
        public Challenge Challenge { get; set; } = null!;

        //Entity properties
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

    }
}
