namespace RenterDemocracy.Models
{
    public class Apartment : Unit
    {
        public string Floor { get; set; }
        public string RoomNumber { get; set; }

        public override string getFullAddress()
        {
            return "{Address} {Zip}, Unit: {RoomNumber} {City}, {State}";
        }
    }
}
