using Lab.Data.Models;

namespace Forum.Data.Models
{
    public class PostTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
