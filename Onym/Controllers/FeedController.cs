using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Onym.Controllers
{
    public class FeedController : Controller
    {
        public async Task<ViewResult> Index()
        {
            return View();
        }
    }
}