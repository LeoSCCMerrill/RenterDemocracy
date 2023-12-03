namespace RenterDemocracy.Models
{
    public class VotingIssueViewModel
    {
        public IList<VotingIssue> CompletedVotingIssues { get; set; } = new List<VotingIssue>();
        public IList<VotingIssue> OpenVotingIssues { get; set; } = new List<VotingIssue>();
    }
}
