namespace RenterDemocracy.Models
{
    public class RoomInviteCode
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Unit Unit { get; set; }
    }
}
