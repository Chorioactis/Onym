using System.Collections.Generic;

#nullable disable

namespace Onym.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string FileLink { get; set; }

        public virtual ICollection<CommentMedia> CommentMedia { get; set; }
        public virtual ICollection<PublicationMedia> PublicationMedia { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}