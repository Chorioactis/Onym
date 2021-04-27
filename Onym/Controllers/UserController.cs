using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Onym.Controllers
{
    public class UserController : Controller
    {
        public async Task<ViewResult> Index()
        {
            return View();
        }
        public async Task<ViewResult> SignIn()
        {
            return View();
        }
    }
}