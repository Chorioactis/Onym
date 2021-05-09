using System.ComponentModel.DataAnnotations;
using Onym.ViewModels.Feed;

#nullable enable

namespace Onym.ViewModels.User
{
    public class SignUpViewModel
    {
        public string? ReturnUrl { get; set; }

        [Display(Name = "Имя пользователя", Prompt = "Имя пользователя")]
        [Required(ErrorMessage = "Введите имя пользователя.")]
        [DataType(DataType.Text)]
        [StringLength(16, ErrorMessage = "{0} должно быть от {2} до {1} символов.", MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9]+$",
            ErrorMessage = "{0} должно состоять из латиницы и цифр без пробелов.")]
        public string UserName { get; set; }

        [Display(Name = "Электронная почта", Prompt = "Электронная почта")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите электронную почту.")]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Некорректная электронная почта.")]
        public string Email { get; set; }

        [Display(Name = "Пароль", Prompt = "Пароль")]
        [StringLength(22, ErrorMessage = "{0} должен быть от {2} до {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,22}$",
            ErrorMessage = "В пароле должна быть минимум одна цифра и буква верхнего и нижнего регистра.")]
        [Required(ErrorMessage = "Введите пароль.")]
        public string Password { get; set; }

        [Display(Name = "Подтвердите пароль", Prompt = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Повторите пароль.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}