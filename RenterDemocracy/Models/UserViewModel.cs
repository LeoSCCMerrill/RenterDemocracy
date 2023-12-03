using Microsoft.AspNetCore.Identity;
namespace RenterDemocracy.Models
{
    public class UserViewModel
    {
        public IList<User> Users { get; set; } = new List<User>();
        public IList<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
    }
}
