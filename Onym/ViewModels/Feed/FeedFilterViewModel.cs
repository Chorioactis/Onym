using System.ComponentModel.DataAnnotations;

#nullable enable

namespace Onym.ViewModels.Feed
{
    public class FeedFilterViewModel
    {
        public FeedFilterViewModel(string tags, string? searchString, int? minRating, int? maxRating, string? oldestCreationDate, string? newestCreationDate)
        {
            Tags = tags;
            SearchString = searchString;
            MinRating = minRating;
            MaxRating = maxRating;
            OldestCreationDate = oldestCreationDate;
            NewestCreationDate = newestCreationDate;
        }

        public FeedFilterViewModel()
        {
        }

        [Display(Name = "Введите метки через запятую", Prompt = "Введите метки через запятую")]
        public string? Tags { get; set; }
        public string? SearchString { get; set; }
        
        [Display(Name = "Минимальный рейтинг", Prompt = "Минимальный рейтинг")]
        public int? MinRating { get; set; }
        [Display(Name = "Максимальный рейтинг", Prompt = "Максимальный рейтинг")]
        public int? MaxRating { get; set; }
        
        public string? OldestCreationDate { get; set; }
        
        public string? NewestCreationDate { get; set; }
    }
}