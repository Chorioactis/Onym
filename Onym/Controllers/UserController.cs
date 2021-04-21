using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onym.Models;

namespace Onym.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
    }
}