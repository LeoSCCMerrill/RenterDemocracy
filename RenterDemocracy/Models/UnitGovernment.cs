namespace RenterDemocracy.Models
{
    public class UnitGovernment
    {
        public Unit unit { get; set; } = new Unit();
        public IList<User> users { get; set; } = new List<User>();
    }
}
