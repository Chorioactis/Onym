#nullable disable

namespace Onym.Models
{
    public class CommentMedia
    {
        public int CommentId { get; set; }
        public int MediaId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Media Media { get; set; }
    }
}