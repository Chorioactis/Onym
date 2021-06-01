using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Onym.ViewModels.Feed
{
    public class AddPostViewModel
    {
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок.")]
        [RegularExpression(@"^.{1,100}$",
            ErrorMessage = "Заголовок должен быть до 100 символов.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Текст публикации", Prompt = "Текст публикации")]
        public string? Content { get; set; }
        
        [Display(Name = "Введите метки через запятую", Prompt = "Введите метки через запятую")]
        [Required(ErrorMessage = "Должна быть хотя бы одна метка.")]
        public string? Tags { get; set; }
        public IFormFileCollection? Uploads { get; set; }
        public string UrlSlug = null;
        public bool PostCreated = false;
    }
}