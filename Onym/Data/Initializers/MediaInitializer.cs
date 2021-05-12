using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Onym.Models;

namespace Onym.Data.Initializers
{
    public class MediaInitializer
    {
        public static async Task InitializeAsync(OnymDbContext<User> db)
        {
            const string avatarPath = "/media/user-avatar-placeholder";
            if (await db.Media.FirstOrDefaultAsync(m => m.FileLink == avatarPath) == null)
            {
                var avatar = new Media{FileLink = avatarPath};
                await db.Media.AddAsync(avatar);
            }
            await db.SaveChangesAsync();
        }
    }
}