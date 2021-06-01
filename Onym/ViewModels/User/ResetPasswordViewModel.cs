using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Onym.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Новый пароль", Prompt = "Новый пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите новый пароль.")]
        [StringLength(22, ErrorMessage = "{0} должен быть от {2} до {1} символов.", MinimumLength = 6)]
        [RegularExpression(
            @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,22}$",
            ErrorMessage = "В пароле должна быть минимум одна цифра и буква верхнего и нижнего регистра.")]
        public string NewPassword { get; set; }
        [Display(Name = "Подтвердите новый пароль", Prompt = "Подтвердите новый пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите новый пароль.")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmNewPassword { get; set; }
        [BindProperty(Name = "token", SupportsGet = true)]
        public string Token { get; set; }
        [BindProperty(Name = "userId", SupportsGet = true)]
        public string UserId { get; set; }
        public bool PasswordChanged = false;
        public bool Error = false;
    }
}