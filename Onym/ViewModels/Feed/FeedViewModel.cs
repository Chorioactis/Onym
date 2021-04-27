using System.Collections.Generic;
using Onym.Models;

#nullable enable

namespace Onym.ViewModels.Feed
{
    public class FeedViewModel
    {
        public List<Publication>? Publications { get; set; }
        public List<PublicationMedia>? PublicationMedia { get; set; }
        public List<PublicationTag>? Tags { get; set; }
        public List<PublicationRatingTally>? PublicationRatingTallies { get; set; }
        public List<Favorite>? Favorites { get; set; }
       
        public FeedFilterViewModel? FeedFilterViewModel { get; set; }
        public FeedPageViewModel? FeedPageViewModel { get; set; }
        public FeedSortViewModel? FeedSortViewModel { get; set; }
        public FeedRateViewModel? FeedRateViewModel { get; set; }
        public FeedModerateViewModel? FeedModerateViewModel { get; set; }
    }
}