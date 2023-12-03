namespace RenterDemocracy.Models
{
    public class Invite
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public User FromUser { get; set; } = new User();
        public string ToUserEmail { get; set; } = string.Empty;
        public Unit ToUnit { get; set; } = new Unit();
    }
}
