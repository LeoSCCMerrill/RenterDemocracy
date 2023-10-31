namespace RenterDemocracy.Models
{
    public class VotingIssue : Post
    {
        public IList<VotingIssueVotes> VoteList {get; set;} = new List<VotingIssueVotes>();
        public bool getOutcome()
        {
            int yay = 0, nay = 0, present = 0, notVoting = 0, total = 0;
            foreach (var item in VoteList)
            {
                switch (item.Vote)
                {
                    case Votes.YAY: yay++; break;
                    case Votes.NAY: nay++; break;
                    case Votes.PRESENT: present++; break;
                    case Votes.ABSTAIN: case Votes.ABSENT: notVoting++; break;
                }
                total++;
            }
            total -= notVoting;
            return yay > total / 2;
        }

    }
}
