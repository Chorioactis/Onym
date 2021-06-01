using System;
using Onym.Data.Enums;

namespace Onym.ViewModels.Feed
{
    public class FeedPageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        
        public FeedAction FeedAction { get; set; }
        public FeedPageViewModel(int count, int pageNumber, int pageSize, FeedAction feedAction)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            FeedAction = feedAction;
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}