namespace RenterDemocracy.Models
{
    public class House : Unit
    {
        public Decimal BasementSize { get; set; }
        public bool IsFreeStanding { get; set; }
        public Decimal LotSize { get; set; }

        public Decimal getFullFloorSize() { return BasementSize + FloorSize; }
        public Decimal getCombinedSize() { return getFullFloorSize() + LotSize; }
    }
}
