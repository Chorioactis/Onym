﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Onym.Models;

namespace Onym.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles = {"Admin", "Moderator", "User"};
            foreach (var r in roles)
            {
                if (await roleManager.FindByNameAsync(r) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(r));
                }
            }
        }
    }
}