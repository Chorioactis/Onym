using Onym.Data.Enums;

namespace Onym.ViewModels.Feed
{
    public class FeedSortViewModel
    {
        public SortState PublicationNameSort { get; private set; }
        public SortState PublicationAgeSort { get; private set; }
        public SortState PublicationRatingSort { get; private set; }
        public SortState Current { get; private set; }
        
        public FeedSortViewModel(SortState sortOrder)
        {
            PublicationNameSort = sortOrder == SortState.PublicationNameAsc ? SortState.PublicationNameDesc : SortState.PublicationNameAsc;
            PublicationAgeSort = sortOrder == SortState.PublicationAgeAsc ? SortState.PublicationAgeDesc : SortState.PublicationAgeAsc;
            PublicationRatingSort = sortOrder == SortState.PublicationRatingAsc ? SortState.PublicationRatingDesc : SortState.PublicationRatingAsc;
            Current = sortOrder;
        }
    }
}