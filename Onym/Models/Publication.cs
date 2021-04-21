using System;
using System.Collections.Generic;

#nullable disable

namespace Onym.Models
{
    public class Publication
    {
        public Publication()
        {
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
            PublicationMedia = new HashSet<PublicationMedia>();
            PublicationRatingTallies = new HashSet<PublicationRatingTally>();
            PublicationTags = new HashSet<PublicationTag>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int RatingTotal { get; set; }
        public int Status { get; set; }

        public virtual Status PublicationStatusNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<PublicationMedia> PublicationMedia { get; set; }
        public virtual ICollection<PublicationRatingTally> PublicationRatingTallies { get; set; }
        public virtual ICollection<PublicationTag> PublicationTags { get; set; }
    }
}