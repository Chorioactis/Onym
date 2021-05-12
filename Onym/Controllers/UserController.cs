using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Onym.Data;
using Onym.Models;
using Onym.Services;
using Onym.ViewModels.Feed;
using Onym.ViewModels.User;

#nullable enable

namespace Onym.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;  
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly OnymDbContext<User> _db;
        private readonly EmailService _emailService;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, OnymDbContext<User> db, EmailService emailService)
        {
            _db = db;
            _emailService = emailService;
            _roleManager = roleManager;
            _userManager = userManager;  
            _signInManager = signInManager;  
        }  
        
        /* PROFILE */
        [HttpGet, AllowAnonymous]
        [Route("@{userName}")]
        public IActionResult Index(string userName)
        {
            ViewBag.SoughtUserName = userName;
            return View();
        }
        
        /* SIGN-IN */
        [HttpGet, AllowAnonymous]
        [Route("sign_in")]
        public IActionResult SignIn(string? returnUrl)
        {
            var model = new SignInViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("sign_in")]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? searchString, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
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
            await _userManager.AccessFailedAsync(user);
            return View(model);
        }

        /* SIGN-UP */
        [HttpGet, AllowAnonymous]
        [Route("sign_up")]
        public IActionResult SignUp(string? returnUrl)
        {
            var model = new SignUpViewModel
            {
                ReturnUrl = returnUrl
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
                        ModelState.AddModelError("UserName", "Что-то пошло не так, попробуйте снова");
                        return View(model);
                    }
                    await _userManager.AddToRoleAsync(user, "User");
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

        /* SETTINGS */
        [HttpGet, Authorize]
        [ImportModelState]
        [Route("settings")]
        public IActionResult Settings()
        {
            ViewBag.SoughtUserName = User.Identity!.Name!;
            SettingsViewModel model;
            if (TempData["model"] == null)
            {
                model = new SettingsViewModel
                {
                    PasswordSettingsViewModel = new PasswordSettingsViewModel(),
                    EmailSettingsViewModel = new EmailSettingsViewModel()
                };
            }
            else
            {
                model = JsonConvert.DeserializeObject<SettingsViewModel>((string) TempData["model"]);
            }
            return View(model);
        }
        
        /* CHANGE PASSWORD */
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("settings/password_change")]
        public async Task<IActionResult> ChangePassword(SettingsViewModel model)
        {
            model.EmailSettingsViewModel = new EmailSettingsViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (await _userManager.CheckPasswordAsync(user, model.PasswordSettingsViewModel.NewPassword))
            {
                model.PasswordSettingsViewModel!.FormShown = true;
                ModelState.AddModelError("PasswordSettingsViewModel.NewPassword", "Новый пароль должен отличаться от текущего.");
                TempData["model"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("Settings", "User");
            }
            var result = await _userManager.ChangePasswordAsync(user, model.PasswordSettingsViewModel.CurrentPassword, model.PasswordSettingsViewModel.NewPassword);
            if (!result.Succeeded)
            {
                model.PasswordSettingsViewModel!.FormShown = true;
                ModelState.AddModelError("PasswordSettingsViewModel.CurrentPassword", "Неверный пароль.");
                TempData["model"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("Settings", "User");
            }
            model.PasswordSettingsViewModel!.CurrentPassword = null;
            model.PasswordSettingsViewModel!.PasswordChanged = true;
            model.PasswordSettingsViewModel!.FormShown = true;
            TempData["model"] = JsonConvert.SerializeObject(model);
            return RedirectToAction("Settings", "User");
        }

        /* FORGOT PASSWORD */
        [HttpGet, AllowAnonymous]
        [Route("forgot_password")]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
            
        }
        
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("forgot_password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                model.EmailSended = true;
                return View(model);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "User", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(model.Email, "Подтвердите сброс пароля.",
                $"Для сброса пароля пройдите по <a href='{callbackUrl}'>ссылке</a>.");
            model.EmailSended = true;
            return View(model);
        }
        
        /* RESET PASSWORD */
        [HttpGet, AllowAnonymous]
        [Route("reset_password")]
        public IActionResult ResetPassword(string? userId, string? token)
        {
            if(token == null)
            {
                return View("_Error");
            }
            var model = new ResetPasswordViewModel()
            {
                UserId = userId,
                Token = token
            };
            return View(model);
        }
 
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        [Route("reset_password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                model.Error = true;
                return View(model);
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                model.Error = true;
                return View(model);
            }
            model.PasswordChanged = true;
            return View(model);
        }
        
        /* CHANGE EMAIL */
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("settings/email_change")]
        public async Task<IActionResult> ChangeEmail(SettingsViewModel model)
        {
            model.PasswordSettingsViewModel = new PasswordSettingsViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (model.EmailSettingsViewModel!.NewEmail == user.Email)
            {
                model.EmailSettingsViewModel.FormShown = true;
                ModelState.AddModelError("EmailSettingsViewModel.NewEmail", "Новая почта должна отличаться от текущей.");
                TempData["model"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("Settings", "User");
            }
            if (!_userManager.CheckPasswordAsync(user, model.EmailSettingsViewModel.CurrentPassword).Result)
            {
                model.EmailSettingsViewModel!.FormShown = true;
                ModelState.AddModelError("EmailSettingsViewModel.CurrentPassword", "Неверный пароль.");
                TempData["model"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("Settings", "User");
            }
            user.Email = model.EmailSettingsViewModel.NewEmail;
            user.EmailConfirmed = false;
            await _userManager.UpdateAsync(user);    
            await _userManager.UpdateNormalizedEmailAsync(user);
            model.EmailSettingsViewModel!.EmailChanged = true;
            model.EmailSettingsViewModel!.FormShown = true;
            TempData["model"] = JsonConvert.SerializeObject(model);
            return RedirectToAction("Settings", "User");
        }
        
        /* EMAIL CONFIRM */
        [HttpGet, AllowAnonymous]
        [Route("settings/email_confirm")]
        public async Task<IActionResult> ConfirmEmail(string? userId, string? token)
        {
            if (userId == null || token == null) return View("_Error");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return View("_Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded) return RedirectToAction("Index", "Feed");
            return View("_Error");
        }
        
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        [ExportModelState]
        [Route("settings/email_confirm")]
        public async Task<IActionResult> ConfirmEmail(SettingsViewModel model)
        {
            model.PasswordSettingsViewModel = new PasswordSettingsViewModel();
            model.EmailSettingsViewModel = new EmailSettingsViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(    
                "ConfirmEmail",
                "User",
                new { userId = user.Id, token = token },
                protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailAsync(user.Email, "Подтвердите вашу почту.",
                $"Подтвердите вашу электронную почту, перейдя по <a href='{callbackUrl}'>ссылке</a>.");
            model.EmailSettingsViewModel!.EmailSended = true;
            model.EmailSettingsViewModel!.FormShown = true;
            TempData["model"] = JsonConvert.SerializeObject(model);
            return RedirectToAction("Settings", "User");
        }
    }
}