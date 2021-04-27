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
            Tags = new SelectList(tags);
            SearchString = searchString;
            SelectedMinRating = minRating;
            SelectedMaxRating = maxRating;
            SelectedOldestCreationDate = oldestCreationDate;
            SelectedNewestCreationDate = newestCreationDate;
        }

        public FeedFilterViewModel(string? searchString)
        {
            SearchString = searchString;
        }

        public SelectList? Tags { get; private set; }
        public string? SearchString { get; private set; }
        public int? SelectedMinRating { get; private set; }
        public int? SelectedMaxRating { get; private set; }
        
        public DateTime? SelectedOldestCreationDate { get; private set; }
        
        public DateTime? SelectedNewestCreationDate { get; private set; }
    }
}