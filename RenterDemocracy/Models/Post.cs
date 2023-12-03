namespace RenterDemocracy.Models
{
    public class Post
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Time { get; set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public User User { get; set; } = new User();
        public Unit Unit { get; set; } = new Unit();
        public IList<Comment> Comments { get; set; } = new List<Comment>();
    }
}
