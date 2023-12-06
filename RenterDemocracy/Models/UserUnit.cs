using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RenterDemocracy.Models
{
    public class UserUnit
    {
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public string UnitId { get; set; } = string.Empty;
        public Unit? Unit { get; set; }
    }
}