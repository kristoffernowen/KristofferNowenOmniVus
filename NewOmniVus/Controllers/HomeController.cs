using Microsoft.AspNetCore.Mvc;
using NewOmniVus.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Services;

namespace NewOmniVus.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        private readonly SecondDbContext _secondDbContext;
        private readonly ProfileManager _profileManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext, SecondDbContext secondDbContext, ProfileManager profileManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            _secondDbContext = secondDbContext;
            _profileManager = profileManager;
        }


        // private readonly ILogger<HomeController> _logger;
        //
        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        public async Task<IActionResult> Index()
        {
            var displayUser = new SignInModel();
            if (_signInManager.IsSignedIn(User))
            {
                displayUser.DisplayName = await _profileManager.GetProfileDisplayNameAsync(User.Identity?.Name);
            }

            return View(displayUser);
        }
        [Authorize]
        public async Task<IActionResult> Contact()
        {

            var displayUser = new SignInModel();
            if (_signInManager.IsSignedIn(User))
            {
                displayUser.DisplayName = await _profileManager.GetProfileDisplayNameAsync(User.Identity?.Name);
            }

            return View(displayUser);
        }

        [Authorize]
        public async Task<IActionResult> Services()
        {
            var displayUser = new SignInModel();
            if (_signInManager.IsSignedIn(User))
            {
                displayUser.DisplayName = await _profileManager.GetProfileDisplayNameAsync(User.Identity?.Name);
            }

            return View(displayUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> TestOfDb()
        {
            
        
            
        
            return View();
        
        
        }
    }
}