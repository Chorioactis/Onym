using Onym.Data.Enums;

namespace Onym.ViewModels.Feed
{
    public class FeedSortViewModel
    {
        public SortState PublicationNameSort { get; set; }
        public SortState PublicationAgeSort { get; set; }
        public SortState PublicationRatingSort { get; set; }
        public SortState Current { get; set; }
        
        public FeedSortViewModel(SortState sortOrder)
        {
            PublicationNameSort = sortOrder == SortState.PublicationNameAsc ? SortState.PublicationNameDesc : SortState.PublicationNameAsc;
            PublicationAgeSort = sortOrder == SortState.PublicationAgeAsc ? SortState.PublicationAgeDesc : SortState.PublicationAgeAsc;
            PublicationRatingSort = sortOrder == SortState.PublicationRatingAsc ? SortState.PublicationRatingDesc : SortState.PublicationRatingAsc;
            Current = sortOrder;
        }
    }
}