using System.Collections.Generic;

#nullable disable

namespace Onym.Models
{
    public class Tag
    {
        public Tag()
        {
            PublicationTags = new HashSet<PublicationTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? TagRatingCounting { get; set; }

        public virtual ICollection<PublicationTag> PublicationTags { get; set; }
    }
}