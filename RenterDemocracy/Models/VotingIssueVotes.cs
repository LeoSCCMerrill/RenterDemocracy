namespace RenterDemocracy.Models
{
    public class VotingIssueVotes
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Votes Vote { get; set; }
    }
}
