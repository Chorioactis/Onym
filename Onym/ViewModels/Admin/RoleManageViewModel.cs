using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Onym.ViewModels.Admin
{
    public class RoleManageViewModel
    {
        public RoleManageViewModel()
        {
            AllRoles = new List<IdentityRole<int>>();
            UserRoles = new List<string>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<IdentityRole<int>> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        
    }
}