using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;
using NewOmniVus.Services;

namespace NewOmniVus.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly SecondDbContext _secondDbContext;
        // private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AddressManager _addressManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(AppDbContext appDbContext, SecondDbContext secondDbContext, AddressManager addressManager, UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            _secondDbContext = secondDbContext;
            _addressManager = addressManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserProfileEntity = await _secondDbContext.Profiles
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.Id == id);

            var userRole = await _appDbContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == id);
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);

            var imageModel = await _secondDbContext.ProfileImages.FirstOrDefaultAsync(i => i.UserId == id);

            if (appUserProfileEntity == null)
            {
                return NotFound();
            }

            var displayModel = new DisplayModel
            {
                FirstName = appUserProfileEntity.FirstName,
                LastName = appUserProfileEntity.LastName,
                AddressLine = appUserProfileEntity.Address.AddressLine,
                PostalCode = appUserProfileEntity.Address.PostalCode,
                City = appUserProfileEntity.Address.City,
                Email = appUserProfileEntity.UserEmail,
                Role = role.Name,
                
            };

            if (imageModel != null)
            {
                displayModel.ImageFileName = imageModel.FileName;
            }

            // if (returnUrl != null)
            //     editModel.ReturnUrl = returnUrl;
            // else
            // {
            //     editModel.ReturnUrl = "/";
            // }

            return View(displayModel);
        }
        public async Task<IActionResult> Index(string returnUrl = null)
        {

            var secondDbContext = _secondDbContext.Profiles.Include(a => a.Address);
            return View(await secondDbContext.ToListAsync());

            
        }
        


       
        [Authorize]
        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            if(!User.IsInRole("Admin"))
                if (!User.FindFirst("Id").Value.Equals(id))
                    return RedirectToAction("Denied", "Authentication");   // ska bli access denied vyn...

            var model = await _secondDbContext.Profiles.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
            
            var imageModel = await _secondDbContext.ProfileImages.FirstOrDefaultAsync(i => i.UserId == id);

            var userRole = await _appDbContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == id);
            var role = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);

            var editModel = new EditAppUserProfile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.UserEmail,
                AddressLine = model.Address.AddressLine,
                PostalCode = model.Address.PostalCode,
                City = model.Address.City,
                Role = role.Name,
                CurrentPassword = "",
                NewPassword = ""
            };

            

            if (imageModel != null)
            {
                editModel.ImageFileName = imageModel.FileName;
            }



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
        public async Task<IActionResult> Edit(string id, EditAppUserProfile profileModel, string returnUrl = null)
        {
            // if (!User.FindFirst("Id").Value.Equals(id))
            //     return Unauthorized();

            if (id == null)
            {
                return NotFound();
            }

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            var originalProfile =

            await _secondDbContext.Profiles.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);

            originalProfile.FirstName = profileModel.FirstName;
            originalProfile.LastName = profileModel.LastName;

            if(profileModel.CurrentPassword != null || profileModel.NewPassword != null)
                await _userManager.ChangePasswordAsync(user, profileModel.CurrentPassword, profileModel.NewPassword);


            // kan jag nog flytta till addressmanager delen

            var userAddressId =
                await _secondDbContext.Profiles.FirstOrDefaultAsync(x => x.Id.Equals(User.FindFirst("Id").Value));
            var howManyId = await _secondDbContext.Profiles.Where(a => a.AddressId == userAddressId.AddressId).ToListAsync();


            if (howManyId.Count > 1)
            {

                var createThisAddress = new AppAddressEntity
                {
                    AddressLine = profileModel.AddressLine,
                    PostalCode = profileModel.PostalCode,
                    City = profileModel.City,
                };
                originalProfile.AddressId = await _addressManager.CreateUserAddressAsync(createThisAddress);
            }
            else if (howManyId.Count <= 1)
            {
                originalProfile.Address.AddressLine = profileModel.AddressLine;
                originalProfile.Address.PostalCode = profileModel.PostalCode;
                originalProfile.Address.City = profileModel.City;
            }

            //_secondDbContext.Entry(originalProfile).State = EntityState.Modified;


            if (ModelState.IsValid)  //Här är den Hans körde. självgenererad?
            {
                try
                {
                    _secondDbContext.Update(originalProfile);
                    await _secondDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserProfileEntityExists(originalProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }


            //await _secondDbContext.SaveChangesAsync();

            if (returnUrl != null)
                profileModel.ReturnUrl = returnUrl;
            else
            {
                profileModel.ReturnUrl = "/";
            }

            return View(profileModel);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserProfileEntity = await _secondDbContext.Profiles
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUserProfileEntity == null)
            {
                return NotFound();
            }

            return View(appUserProfileEntity);
        }

        // POST: AppUserProfileEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appUserProfileEntity = await _secondDbContext.Profiles.FindAsync(id);
            var userIdentity = await _appDbContext.Users.FindAsync(id);
            var userRole = await _appDbContext.UserRoles.FirstOrDefaultAsync(x=> x.UserId == id);

            _secondDbContext.Profiles.Remove(appUserProfileEntity);
            await _secondDbContext.SaveChangesAsync();

            _appDbContext.Users.Remove(userIdentity);
            await _appDbContext.SaveChangesAsync();

            _appDbContext.UserRoles.Remove(userRole);

            return RedirectToAction("Index", "Profile");
        }
        private bool AppUserProfileEntityExists(string id)
        {
            return _secondDbContext.Profiles.Any(e => e.Id.Equals(id));
        }
    }
}
