using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;
using NewOmniVus.Services;

namespace NewOmniVus.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly SecondDbContext _secondDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AddressManager _addressManager;

        public ProfileController(SecondDbContext secondDbContext, SignInManager<IdentityUser> signInManager, AddressManager addressManager)
        {
            _secondDbContext = secondDbContext;
            _signInManager = signInManager;
            _addressManager = addressManager;
        }

        public async Task<IActionResult> Index(string returnUrl = null)
        {

            var model = await _secondDbContext.Profiles.Include(x => x.Address).FirstOrDefaultAsync(x => x.UserEmail == User.Identity.Name);

            var editModel = new EditAppUserProfile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                AddressLine = model.Address.AddressLine,
                PostalCode = model.Address.PostalCode,
                City = model.Address.City,
                OldCity = model.Address.City

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
            
            await _secondDbContext.Profiles.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.UserEmail == User.Identity.Name);

            originalProfile.FirstName = profileModel.FirstName;
            originalProfile.LastName = profileModel.LastName;

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
            else if(howManyId.Count <= 1)
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
        private bool AppUserProfileEntityExists(string id)
        {
            return _secondDbContext.Profiles.Any(e => e.Id.Equals(id));
        }
    }
}
