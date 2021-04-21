using System;
using System.Collections.Generic;

#nullable disable

namespace Onym.Models
{
    public class Comment
    {
        public Comment()
        {
            CommentMedia = new HashSet<CommentMedia>();
            CommentRatingTallies = new HashSet<CommentRatingTally>();
            InverseParentalComment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int? ParentalCommentId { get; set; }
        public int UserId { get; set; }
        public int PublicationId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int RatingTotal { get; set; }
        public int Status { get; set; }

        public virtual Status CommentStatusNavigation { get; set; }
        public virtual Comment ParentalComment { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommentMedia> CommentMedia { get; set; }
        public virtual ICollection<CommentRatingTally> CommentRatingTallies { get; set; }
        public virtual ICollection<Comment> InverseParentalComment { get; set; }
    }
}