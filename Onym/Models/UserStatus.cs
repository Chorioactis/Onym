using System;

#nullable disable

namespace Onym.Models
{
    public class UserStatus
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime ExpirationTime { get; set; }

        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}