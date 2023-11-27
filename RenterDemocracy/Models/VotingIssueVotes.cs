namespace RenterDemocracy.Models
{
    public class VotingIssueVotes
    {
        public string Id { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public Votes Vote { get; set; }
    }
}
