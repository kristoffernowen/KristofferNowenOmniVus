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

            // var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == model.UserEmail);
            //
            // await _userManager.AddToRoleAsync(user, model.RoleName);

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {


            var model = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            var editModel = new EditRoleModel()
            {
               RoleName = model.Name,
                


            };

            if (returnUrl != null)
                editModel.ReturnUrl = returnUrl;
            else
            {
                editModel.ReturnUrl = "/";
            }


            return View(editModel);
        }
        // [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditRoleModel roleModel, string returnUrl = null)
        {
            // if (!User.FindFirst("Id").Value.Equals(id))
            //     return Unauthorized();

            if (id == null)
            {
                return NotFound();
            }

            var originalRole =

                await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

            originalRole.Name = roleModel.RoleName;
            

            
           


            if (ModelState.IsValid)  //Här är den Hans körde. självgenererad?
            {
                try
                {
                    _appDbContext.Update(originalRole);
                    await _appDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // if (!AppUserProfileEntityExists(originalProfile.Id))
                    // {
                    //     return NotFound();
                    // }
                    // else
                    // {
                    //     throw;
                    // }
                }
                return RedirectToAction("Index", "Role");
            }


            

            if (returnUrl != null)
                roleModel.ReturnUrl = returnUrl;
            else
            {
                roleModel.ReturnUrl = "/";
            }

            return View();
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleEntityy = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (roleEntityy == null)
            {
                return NotFound();
            }

            return View(roleEntityy);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            
            var roleEntity = await _appDbContext.Roles.FindAsync(id);
            if (!roleEntity.Name.Equals("Admin"))
            {
                _appDbContext.Roles.Remove(roleEntity);
                await _appDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Role");
        }


    }
}
