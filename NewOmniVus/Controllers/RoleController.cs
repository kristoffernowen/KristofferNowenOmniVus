using KristofferNowen_OmniVus.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;

namespace NewOmniVus.Controllers
{
    [Authorize(Policy = "Admins")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SecondDbContext _secondDbContext;

        public RoleController(AppDbContext appDbContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SecondDbContext secondDbContext)
        {
            _appDbContext = appDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _secondDbContext = secondDbContext;
        }


        public async Task<IActionResult> Index()
        {
            var roles = await _appDbContext.Roles.ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> CreateRole()
        {
            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            await _roleManager.CreateAsync(new IdentityRole(model.RoleName));

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == model.UserEmail);

            await _userManager.AddToRoleAsync(user, model.RoleName);

            return View();
        }
    }
}
