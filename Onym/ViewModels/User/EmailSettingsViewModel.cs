using System.ComponentModel.DataAnnotations;

namespace Onym.ViewModels.User
{
    public class EmailSettingsViewModel
    {
        [Display(Name = "Текущий пароль", Prompt = "Текущий пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите текущий пароль.")]
        [StringLength(22, ErrorMessage = "{0} должен быть от {2} до {1} символов.", MinimumLength = 6)]
        public string CurrentPassword { get; set; }
        [Display(Name = "Новая электронная почта", Prompt = "Новая электронная почта")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите новую электронную почту.")]
        [RegularExpression(
            @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "Некорректная электронная почта.")]
        public string NewEmail { get; set; }

        public bool EmailChanged = false;
        public bool EmailSended = false;
        public bool FormShown = false;
    }
}