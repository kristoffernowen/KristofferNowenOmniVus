using Microsoft.AspNetCore.Mvc;
using NewOmniVus.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;

namespace NewOmniVus.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly SecondDbContext _secondDbContext;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, SecondDbContext secondDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _secondDbContext = secondDbContext;
        }


        // private readonly ILogger<HomeController> _logger;
        //
        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            return View();


        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // public async Task<IActionResult> TestOfDb()
        // {
        //     var koffesEmail = "koffe@koffe.se";
        //     
        //
        //     var koffeUser = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email == koffesEmail);
        //     var koffeSecond = await _secondDbContext.SecondUsers.SingleOrDefaultAsync(x => x.Email == koffesEmail);
        //
        //     var koffe = new TestModel();
        //
        //     koffe.Name = koffeSecond.Name;
        //     koffe.UserId = koffeUser.Id;
        //
        //     return View(koffe);
        //
        //
        // }
    }
}