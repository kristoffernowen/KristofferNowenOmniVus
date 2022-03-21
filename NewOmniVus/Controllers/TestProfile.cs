using Microsoft.AspNetCore.Mvc;

namespace NewOmniVus.Controllers
{
    public class TestProfile : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
