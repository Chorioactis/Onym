using System.Collections.Generic;
using Onym.Data.Enums;
using Onym.Models;

#nullable enable

namespace Onym.ViewModels.Feed
{
    public class FeedViewModel
    {
        public List<Publication>? Publications { get; set; }
        public Publication? Publication { get; set; }
        public PublicationRatingTally? Tally { get; set; }
        public Favorite? Favorite { get; set; }
        public FeedFilterViewModel? FeedFilterViewModel { get; set; }
        public FeedPageViewModel? FeedPageViewModel { get; set; }
        public FeedSortViewModel? FeedSortViewModel { get; set; }
        public FeedRateViewModel? FeedRateViewModel { get; set; }
        public FeedModerateViewModel? FeedModerateViewModel { get; set; }
    }
}