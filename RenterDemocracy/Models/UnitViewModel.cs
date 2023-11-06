namespace RenterDemocracy.Models
{
    public class UnitViewModel
    {
        public IList<House> Houses { get; set; } = new List<House>();
        public IDictionary<String, IList<Apartment>> Apartments { get; set; } = new Dictionary<String, IList<Apartment>>();
    }
}
