using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RenterDemocracy.Models
{
    public class Unit
    {
        public string Id { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public decimal Bedrooms { get; set; }
        public decimal Bathrooms { get; set; }
        public decimal Levels { get; set; }
        public decimal FloorSize { get; set; }
        public Owner Owner { get; set; } = new Owner();
        public int CoveredDriveway { get; set; } = 0;
        public int UncoveredDriveway {get;set;} =0;
        public int CoveredLot { get; set; } = 0;
        public int UncoveredLot { get; set; } = 0;
        public int Garage { get; set; } = 0;
        public int Street { get; set; } = 0;
        public IList<UserUnit>? UserUnits { get; set; }
        public IList<User>? Users { get; set; }

        public virtual string getFullAddress()
        {
            return "{Address} {Zip}, {City}, {State}";
        }
    }
}
