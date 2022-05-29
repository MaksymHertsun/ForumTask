using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Name { get; set; }
        public List<Post> Posts { get; set; } = new();
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public Topic()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
