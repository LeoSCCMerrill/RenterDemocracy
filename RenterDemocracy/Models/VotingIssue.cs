namespace RenterDemocracy.Models
{
    public class VotingIssue : Post
    {
        public IList<VotingIssueVotes> VotingIssueVotes {get; set;} = new List<VotingIssueVotes>();
        public IList<User> VotingUsers { get; set;} = new List<User>();
        public bool Completed { get; set; } = false;
        public string GetOutcome()
        {
            int yay = 0, nay = 0,total = 0;
            foreach (VotingIssueVotes votingIssueVotes in VotingIssueVotes)
            {
                switch (votingIssueVotes.Vote)
                {
                    case Votes.YAY: yay++; total++; break;
                    case Votes.NAY: nay++; total++; break;
                    case Votes.PRESENT: total++; break;
                }
            }

            if(yay > total / 2)
            {
                return "Passed";
            }
            return "Failed";
        }

    }
}
