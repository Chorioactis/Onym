using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onym.Data;
using Onym.Models;

#nullable enable

namespace Onym.Controllers
{
    public class FeedController : Controller
    {
        private static OnymDbContext<User>? _db;
        
        //CLASS CONSTRUCTOR
        public FeedController(OnymDbContext<User> context)
        {
            _db = context;
        }

        [HttpGet]
        [AllowAnonymous]
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
    }
}