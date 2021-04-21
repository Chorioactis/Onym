using System.ComponentModel.DataAnnotations;

namespace Onym.ViewModels.User
{
    public class SignUpViewModel
    {
        [Display(Name = "Логин", Prompt = "Логин")]
        [Required(ErrorMessage = "Введите логин.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])\S{3,20}$",
            ErrorMessage = "Логин должен быть от 3 до 20 символов латиницей без пробелов, минимум одна буква.")]
        public string UserLogin { get; set; }

        [Display(Name = "Электронная почта", Prompt = "Электронная почта")]
        [Required(ErrorMessage = "Введите электронную почту.")]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Некорректная электронная почта.")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Пароль", Prompt = "Пароль")]
        [Required(ErrorMessage = "Введите пароль.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)\S{6,20}$",
            ErrorMessage = "Пароль должен быть от 6 до 20 символов и состоять из цифр и минимум 1 буквы без пробелов.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтвердите пароль", Prompt = "Подтвердите пароль")]
        [Required(ErrorMessage = "Повторите пароль.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}