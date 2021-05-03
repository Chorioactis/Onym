using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onym.Data;
using Onym.Models;

#nullable enable

namespace Onym.Controllers
{
    public class OtherController : Controller
    {
        private readonly UserManager<User> _userManager;  
        private readonly SignInManager<User> _signInManager;  
        private readonly OnymDbContext<User> _db;
        public OtherController(UserManager<User> userManager, SignInManager<User> signInManager, OnymDbContext<User> db)
        {
            _db = db;
            _userManager = userManager;  
            _signInManager = signInManager;  
        }  

        public async Task<IActionResult> Rules()
        {
            return null;
        }
        public async Task<ViewResult> Support()
        {
            return null;
        }
    }
}