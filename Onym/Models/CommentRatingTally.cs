#nullable disable

namespace Onym.Models
{
    public class CommentRatingTally
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public bool Rating { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}