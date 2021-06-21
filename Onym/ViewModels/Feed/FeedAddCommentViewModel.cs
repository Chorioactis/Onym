using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Onym.ViewModels.Feed
{
    public class FeedAddCommentViewModel
    {
        [Display(Name = "Текст комментария", Prompt = "Текст комментария")]
        public string? Content { get; set; }
        public IFormFileCollection? Uploads { get; set; }
        public int Publication { get; set; }
        public int? ParentalComment { get; set;}
    }
}