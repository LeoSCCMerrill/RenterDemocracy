using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RenterDemocracy.Models
{
    public class Unit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Address { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public decimal Bedrooms { get; set; }
        public decimal Bathrooms { get; set; }
        public decimal Levels { get; set; }
        public int FloorSize { get; set; }
        public User Owner { get; set; } = new User();
        public UnitType UnitType { get; set; }
        public IList<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
        public IList<User> Users { get; set; } = new List<User>();
        public string? UnitNumber { get; set; }

        public virtual string GetFullAddress()
        {
            return $"{Address} {Zip}, {City}, {State}";
        }

        public virtual string GetBedBath() {
            return $"{Bedrooms} bed, {Bathrooms} bath, {Levels} levels, {FloorSize}, square feet"; 
        }
    }
}
