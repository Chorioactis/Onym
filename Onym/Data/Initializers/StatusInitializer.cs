using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Onym.Models;

namespace Onym.Data.Initializers
{
    public class StatusInitializer
    {
        public static async Task InitializeAsync(OnymDbContext<User> db)
        {
            string[] statuses = {"Default", "Hidden"};
            foreach (var s in statuses)
            {
                if (await db.Statuses.FirstOrDefaultAsync(m => m.Name == s) == null)
                {
                    var avatar = new Status() {Name = s};
                    await db.Statuses.AddAsync(avatar);
                }
            }
            await db.SaveChangesAsync();
        }
    }
}