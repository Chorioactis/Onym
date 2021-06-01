using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onym.Models;

#nullable enable

namespace Onym.ViewModels.Feed
{
    public class FeedFilterViewModel
    {
        public FeedFilterViewModel(IEnumerable<Tag>? tags, string? searchString, int? minRating, int? maxRating, DateTime? oldestCreationDate, DateTime? newestCreationDate)
        {
            Tags = new List<Tag>(tags!);
            SearchString = searchString;
            SelectedMinRating = minRating;
            SelectedMaxRating = maxRating;
            SelectedOldestCreationDate = oldestCreationDate;
            SelectedNewestCreationDate = newestCreationDate;
        }

        public FeedFilterViewModel()
        {
        }

        public List<Tag>? Tags { get; set; }
        public string? SearchString { get; set; }
        public int? SelectedMinRating { get; set; }
        public int? SelectedMaxRating { get; set; }
        
        public DateTime? SelectedOldestCreationDate { get; set; }
        
        public DateTime? SelectedNewestCreationDate { get; set; }
    }
}