namespace RenterDemocracy.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public Unit Unit { get; set; }
    }
}
