using Microsoft.AspNetCore.Identity;

namespace RenterDemocracy.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; } 
        public IList<IdentityRole> Roles { get; set; } = new List<IdentityRole>();

        public User() { }

        public int getAge()
        {
            if (BirthDate ==  null)
            {
                return -1;
            }
            TimeSpan ts = DateTime.Now.Subtract((DateTime)BirthDate);
            return (int) ts.TotalDays / 365;
        }

        public string getFullName()
        {
            return "{First Name} {MiddleName} {LastName}";
        }
    }
}
