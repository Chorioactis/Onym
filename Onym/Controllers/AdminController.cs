using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onym.Data;
using Onym.Models;
using Onym.ViewModels.Role;

namespace Onym.Controllers
{
    [Authorize(Roles = "Moderator")]
    [Route("admin/")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly OnymDbContext<User> _db;
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, OnymDbContext<User> db)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }
        /*----- ROLES MANAGER -----*/
        /* ROLE LIST */
        [HttpGet, Authorize(Roles = "Moderator")]
        [Route("role/role_list")]
        public IActionResult RoleList()
        {
            return View("Role/RoleList",_roleManager.Roles.ToList());
        }
        
        /* CREATE ROLE */
        [HttpGet, Authorize(Roles = "Admin")]
        [Route("role/create_role")]
        public IActionResult CreateRole()
        {
            return View("Role/CreateRole");
        }

        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        [Route("role/create_role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return View(roleName);
            var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
            if (result.Succeeded) return RedirectToAction("RoleList");
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            return View("Role/CreateRole",roleName);
        }
        
        /* DELETE ROLE */
        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        [Route("role/delete_role")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null) await _roleManager.DeleteAsync(role);
            return RedirectToAction("RoleList");
        }
        
        /* EDIT ROLE */
        [HttpGet, Authorize(Roles = "Admin")]
        [Route("role/edit_role")]
        public async Task<IActionResult> EditRole(string roleId)
        {
            return View("Role/EditRole");
        }
        
        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        [Route("role/edit_role")]
        public async Task<IActionResult> EditRole(string roleId, string roleName)
        {
            return View("Role/EditRole");
        }
        
        /* EDIT USER ROLES */
        [HttpGet, Authorize(Roles = "Admin")]
        [Route("role/edit_user_roles")]
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            var model = new RoleManageViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View("User/EditUserRoles",model);
        }
        
        [HttpPost, Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        [Route("role/edit_user_roles")]
        public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);
 
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("UserList","Admin");
        }
       
        /*------ USER MANAGER -----*/
        /* USER LIST */
        [HttpGet, Authorize]
        [Route("user/user_list")]
        public IActionResult UserList()
        {
            return View("User/UserList",_userManager.Users.ToList());;
        }
    }
}