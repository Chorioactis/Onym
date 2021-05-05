using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onym.Data;
using Onym.Models;
using Onym.ViewModels.Feed;
using Onym.ViewModels.User;

#nullable enable

namespace Onym.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;  
        private readonly SignInManager<User> _signInManager;  
        private readonly OnymDbContext<User> _db;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, OnymDbContext<User> db)
        {
            _db = db;
            _userManager = userManager;  
            _signInManager = signInManager;  
        }  
        
        /* PROFILE */
        [HttpGet, AllowAnonymous]
        [Route("@{login}")]
        public IActionResult Index(string login, string? searchString)
        {
            ViewBag.SoughtUserProfile = login;
            /* FORMING VIEW MODEL */
            var model = new IndexViewModel
            {
                FeedFilterViewModel = new FeedFilterViewModel(searchString),
            };
            return View(model);
        }
        
        /* SIGN-IN */
        [HttpGet, AllowAnonymous]
        [Route("sign_in")]
        public IActionResult SignIn(string? searchString, string? returnUrl)
        {
            /* FORMING VIEW MODEL */
            var model = new SignInViewModel
            {
                FeedFilterViewModel = new FeedFilterViewModel(searchString),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("sign_in")]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? searchString, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            if (await _userManager.FindByNameAsync(model.UserName) == null)
            {
                ModelState.AddModelError("UserName", "Пользователь не найден");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Feed");
            }
            ModelState.AddModelError("Password", "Неверный пароль");
            return View(model);
        }

        /* SIGN-UP */
        [HttpGet, AllowAnonymous]
        [Route("sign_up")]
        public IActionResult SignUp(string? searchString)
        {
            /* FORMING VIEW MODEL */
            var model = new SignUpViewModel
            {
                FeedFilterViewModel = new FeedFilterViewModel(searchString),
            };
            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("sign_up")]
        public async Task<IActionResult> SignUp(SignUpViewModel model, string? searchString)
        {
            if (!ModelState.IsValid) return View(model);
            if (await _userManager.FindByNameAsync(model.UserName) == null)
            {
                if (await _userManager.FindByEmailAsync(model.Email) == null)
                {
                    var user = new User
                    {
                        UserName = model.UserName,
                        NormalizedUserName = model.UserName,
                        Email = model.Email,
                        NormalizedEmail = model.Email,
                        ProfilePicture = 1
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        return View(model);
                    }
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                    
                    return RedirectToAction("Index", "Feed");

                }
                ModelState.AddModelError("Email", "Электронная почта уже используется.");
            }
            else
            {
                ModelState.AddModelError("UserName", "Такой пользователь уже существует.");
            }
            return View(model);
        }
        
        /* LOGOUT */
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [Route("sign_out")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Feed");
        }
    }
}