namespace RenterDemocracy.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; } = new User();
        public Post Post { get; set; } = new Post();
        public string Content { get; set; } = string.Empty;
        public Comment? Reply { get; set; }
    }
}
