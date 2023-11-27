namespace RenterDemocracy.Models
{
    public class UnitParking
    {
        public string Id { get; set; } = string.Empty;
        public string UnitId { get; set; } = string.Empty;
        public Unit Unit { get; set; } = new Unit();
        public ParkingType ParkingType { get; set; }
        public int Spaces { get; set; }
    }
}
