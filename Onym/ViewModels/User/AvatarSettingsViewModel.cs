using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Onym.ViewModels.User
{
    public class AvatarSettingsViewModel
    {
        [Required(ErrorMessage = "Выберите файл.")]
        public IFormFile? Upload { get; set; }
        public bool AvatarChanged = false;
        public bool FormShown = false;
    }
}