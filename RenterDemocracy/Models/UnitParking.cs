namespace RenterDemocracy.Models
{
    public class UnitParking
    {
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = new Unit();
        public ParkingType ParkingType { get; set; }
        public int Spaces { get; set; }
    }
}
