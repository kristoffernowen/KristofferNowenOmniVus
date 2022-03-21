using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models.Profiles;

namespace NewOmniVus.Controllers
{
    
    public class ProfileController : Controller
    {
        private readonly SecondDbContext _secondDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ProfileController(SecondDbContext secondDbContext, SignInManager<IdentityUser> signInManager)
        {
            _secondDbContext = secondDbContext;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index( string returnUrl = null)
        {

            var model = await _secondDbContext.Profiles.Include(x => x.Address).FirstOrDefaultAsync(x => x.UserEmail == User.Identity.Name);

            var editModel = new EditAppUserProfile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                AddressLine = model.Address.AddressLine,
                PostalCode = model.Address.PostalCode,
                City = model.Address.City,
                
            };

            if (returnUrl != null)
                editModel.ReturnUrl = returnUrl;
            else
            {
                editModel.ReturnUrl = "/";
            }

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EditAppUserProfile profileModel, string returnUrl = null)
        {
            var originalProfile =
                await _secondDbContext.Profiles.FirstOrDefaultAsync(x => x.UserEmail == User.Identity.Name);
            

            originalProfile.FirstName = profileModel.FirstName;
            originalProfile.LastName = profileModel.LastName;

            _secondDbContext.Entry(originalProfile).State = EntityState.Modified;
            // _secondDbContext.Update(originalProfile); Är det bara att köra denna istället?

            // if (ModelState.IsValid) Här är den Hans körde. självgenererad?
            // {
            //     try
            //     {
            //         _context.Update(profileEntity);
            //         await _context.SaveChangesAsync();
            //     }
            //     catch (DbUpdateConcurrencyException)
            //     {
            //         if (!ProfileEntityExists(profileEntity.Id))
            //         {
            //             return NotFound();
            //         }
            //         else
            //         {
            //             throw;
            //         }
            //     }
            //     return RedirectToAction(nameof(Index));
            // }


            await _secondDbContext.SaveChangesAsync();

            if (returnUrl != null)
                profileModel.ReturnUrl = returnUrl;
            else
            {
                profileModel.ReturnUrl = "/";
            }

            return View(profileModel);
        }
    }
}
