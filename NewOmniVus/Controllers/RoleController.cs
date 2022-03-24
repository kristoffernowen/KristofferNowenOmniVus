using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;

namespace NewOmniVus.Controllers
{
    public class RoleController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public RoleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Index()
        {
            var roles = await _appDbContext.Roles.ToListAsync();

            return View(roles);
        }
    }
}
