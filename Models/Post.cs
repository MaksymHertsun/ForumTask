namespace Forum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Text { get; set; }
        public Topic? Topic { get; set; }
        public int TopicId { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public Post()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
