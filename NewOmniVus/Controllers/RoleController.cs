using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewOmniVus.Controllers
{
    public class RoleController : Controller
    {
        [Authorize(Policy = "Admins")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
