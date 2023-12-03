using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenterDemocracy.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        [NotMapped]
        public IList<string> RoleNames { get; set; } = new List<string>();
        public IList<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
        public IList<Unit> OwnedUnits { get; set; } = new List<Unit>();
        public IList<Post> PostsAuthored { get; set; } = new List<Post>();
        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<VotingIssueVotes> VotingIssueVotes { get; set; }
        public IList<VotingIssue> VotingIssues { get; set; }

        public User() { }

        public int GetAge()
        {
            if (BirthDate == null)
            {
                return -1;
            }
            TimeSpan ts = DateTime.Now.Subtract((DateTime)BirthDate);
            return (int)ts.TotalDays / 365;
        }

        public string GetFullName()
        {
            return $"{FirstName} {MiddleName} {LastName}";
        }
    }
}
