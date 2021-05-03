using System.ComponentModel.DataAnnotations;
using Onym.ViewModels.Feed;

#nullable enable

namespace Onym.ViewModels.User
{
    public class SignUpViewModel
    {
        public FeedFilterViewModel? FeedFilterViewModel { get; set; }
        
        [Display(Name = "Имя пользователя", Prompt = "Имя пользователя")]
        [Required(ErrorMessage = "Введите имя пользователя.")]
        public string UserName { get; set; }

        [Display(Name = "Электронная почта", Prompt = "Электронная почта")]
        [Required(ErrorMessage = "Введите электронную почту.")]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Некорректная электронная почта.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль", Prompt = "Пароль")]
        [Required(ErrorMessage = "Введите пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтвердите пароль", Prompt = "Подтвердите пароль")]
        [Required(ErrorMessage = "Повторите пароль.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}