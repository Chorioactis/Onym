using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Onym.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            CommentRatingTallies = new HashSet<CommentRatingTally>();
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
            PublicationRatingTallies = new HashSet<PublicationRatingTally>();
            Publications = new HashSet<Publication>();
            UserStatuses = new HashSet<UserStatus>();
        }

        public int ProfilePicture { get; set; }
        public int PasswordSalt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int RatingTotal { get; set; }

        //Navigation properties
        public virtual Media UserProfilePictureNavigation { get; set; }
        public virtual ICollection<CommentRatingTally> CommentRatingTallies { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<PublicationRatingTally> PublicationRatingTallies { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<UserStatus> UserStatuses { get; set; }

        //Identity navigation properties
        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; }
    }
}