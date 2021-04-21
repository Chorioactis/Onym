using System.Collections.Generic;

#nullable disable

namespace Onym.Models
{
    public class Status
    {
        public Status()
        {
            Comments = new HashSet<Comment>();
            Publications = new HashSet<Publication>();
            UserStatuses = new HashSet<UserStatus>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<UserStatus> UserStatuses { get; set; }
    }
}