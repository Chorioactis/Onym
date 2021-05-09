using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onym.Data;
using Onym.Models;

#nullable enable

namespace Onym.Controllers
{
    public class FeedController : Controller
    {
        private readonly UserManager<User> _userManager;  
        private readonly SignInManager<User> _signInManager;  
        private readonly OnymDbContext<User> _db;
        public FeedController(UserManager<User> userManager, SignInManager<User> signInManager, OnymDbContext<User> db)
        {
            _db = db;
            _userManager = userManager;  
            _signInManager = signInManager;  
        }  

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<ViewResult> Best()
        {
            return View();
        }
        public async Task<ViewResult> New()
        {
            return View();
        }
        public async Task<ViewResult> Random()
        {
            return View();
        }
        public async Task<ViewResult> Search()
        {
            return View();
        }
        public async Task<ViewResult> Rated()
        {
            return View();
        }
        public async Task<ViewResult> Favorite()
        {
            return View();
        }
        public async Task<ViewResult> Comments()
        {
            return View();
        }
    }
}