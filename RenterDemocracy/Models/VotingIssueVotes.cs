 namespace RenterDemocracy.Models
{
    public class VotingIssueVotes
    {
        public string VotingIssueId { get; set; } = string.Empty;
        public VotingIssue VotingIssue { get; set; } = new VotingIssue();
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = new User();
        public Votes Vote { get; set; }
    }
}
