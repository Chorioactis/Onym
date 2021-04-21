using System.Collections.Generic;

#nullable disable

namespace Onym.Models
{
    public class Media
    {
        public Media()
        {
            CommentMedia = new HashSet<CommentMedia>();
            PublicationMedia = new HashSet<PublicationMedia>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string FileLink { get; set; }

        public virtual ICollection<CommentMedia> CommentMedia { get; set; }
        public virtual ICollection<PublicationMedia> PublicationMedia { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}