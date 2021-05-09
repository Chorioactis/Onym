using System.ComponentModel.DataAnnotations;
using Onym.ViewModels.Feed;

#nullable enable

namespace Onym.ViewModels.User
{
    public class SignInViewModel
    {
        public string? ReturnUrl { get; set; }
        
        [Display(Name = "Имя пользователя", Prompt = "Имя пользователя")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите имя пользователя.")]
        [StringLength(16, ErrorMessage = "{0} должно быть от {2} до {1} символов.", MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9]+$",
            ErrorMessage = "{0} должно состоять из латиницы и цифр без пробелов.")]
        public string UserName { get; set; }
        
        [Display(Name = "Пароль", Prompt = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль.")]
        [StringLength(22, ErrorMessage = "{0} должен быть от {2} до {1} символов.", MinimumLength = 6)]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить меня.")]
        public bool RememberMe { get; set; }
    }
}