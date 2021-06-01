using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MimeKit.Cryptography;
using Onym.Models;

namespace Onym.ViewModels.Feed
{
    public class EditPostViewModel
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
        public string? NewTags { get; set; }
        public string? OldTags { get; set; }
        public ICollection<PublicationMedia>? OldUploads { get; set; }
        public ICollection<int>? SavedUploads { get; set; }
        public IFormFileCollection? NewUploads { get; set; }
        public int Id { get; set; }
    }
}