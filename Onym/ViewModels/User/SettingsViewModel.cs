#nullable enable

namespace Onym.ViewModels.User
{
    public class SettingsViewModel
    {
        public AvatarSettingsViewModel? AvatarSettingsViewModel { get; set; }
        public PasswordSettingsViewModel? PasswordSettingsViewModel { get; set; }
        public EmailSettingsViewModel? EmailSettingsViewModel { get; set; }
    }
}