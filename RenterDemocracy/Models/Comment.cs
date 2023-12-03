namespace RenterDemocracy.Models
{
    public class Comment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public User User { get; set; } = new User();
        public Post Post { get; set; } = new Post();
        public string Content { get; set; } = string.Empty;
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
