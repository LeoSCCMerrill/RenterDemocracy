namespace RenterDemocracy.Models
{
    public class PostViewModel
    {
        public KeyValuePair<Unit, IList<Post>> Posts { get; set; } = new KeyValuePair<Unit, IList<Post>>();
    }
}
