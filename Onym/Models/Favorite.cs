#nullable disable

namespace Onym.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public int PublicationId { get; set; }

        public virtual Publication Publication { get; set; }
        public virtual User User { get; set; }
    }
}