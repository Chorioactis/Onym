#nullable disable

namespace Onym.Models
{
    public class PublicationMedia
    {
        public int PublicationId { get; set; }
        public int MediaId { get; set; }

        public virtual Media Media { get; set; }
        public virtual Publication Publication { get; set; }
    }
}