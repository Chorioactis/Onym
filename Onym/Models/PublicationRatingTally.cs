#nullable disable

namespace Onym.Models
{
    public class PublicationRatingTally
    {
        public int UserId { get; set; }
        public int PublicationId { get; set; }
        public bool PublicationRating { get; set; }

        public virtual Publication Publication { get; set; }
        public virtual User User { get; set; }
    }
}