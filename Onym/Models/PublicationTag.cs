#nullable disable

namespace Onym.Models
{
    public class PublicationTag
    {
        public int TagId { get; set; }
        public int PublicationId { get; set; }

        public virtual Publication Publication { get; set; }
        public virtual Tag Tag { get; set; }
    }
}