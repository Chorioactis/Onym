#nullable enable

namespace Onym.ViewModels.User
{
    public class SettingsViewModel
    {
        public PasswordSettingsViewModel? PasswordSettingsViewModel { get; set; }
        public EmailSettingsViewModel? EmailSettingsViewModel { get; set; }
    }
}