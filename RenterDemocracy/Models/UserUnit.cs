using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RenterDemocracy.Models
{
    public class UserUnit
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = new Unit();
        public IdentityRole Role { get; set; } = new IdentityRole();
    }
}
