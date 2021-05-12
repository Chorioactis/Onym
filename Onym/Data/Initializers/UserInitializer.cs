using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Onym.Models;

namespace Onym.Data.Initializers
{
    public class UserInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            const string password = "59735651Admin";
            var admin = new User
            {
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "Chorioactis@yandex.ru",
                NormalizedEmail = "CHORIOACTIS@YANDEX.RU",
                ProfilePicture = 1,
            };
            string[] roles = {"Admin", "Moderator", "User"};
            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(admin, roles);
                }
            }
        }
    }
}