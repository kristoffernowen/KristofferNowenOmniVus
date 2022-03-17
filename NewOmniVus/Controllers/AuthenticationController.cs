using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NewOmniVus.Data;
using NewOmniVus.Models;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;
using NewOmniVus.Services;

namespace NewOmniVus.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ProfileManager _profileManager;
        private readonly AddressManager _addressManager;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ProfileManager profileManager, AddressManager addressManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _profileManager = profileManager;
            _addressManager = addressManager;
        }


        public IActionResult SignUp(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = new SignUpModel();

            if (returnUrl != null)
                model.ReturnUrl = returnUrl;
            else
            {
                model.ReturnUrl = "/";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            var roleName = "User";

            if (ModelState.IsValid)
            {
                if (!_userManager.Users.Any())
                    roleName = "Admin";



                var user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                var address = new AppAddress
                {
                    AddressLine = model.AddressLine,
                    PostalCode = model.PostalCode,
                    City = model.City,
                };

                var addressId = await _addressManager.CreateUserAddressAsync(address);


                var userProfile = new SignUpAppUserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserEmail = model.Email,
                    AddressId = addressId
                };


                await _profileManager.CreateProfileAsync(userProfile);


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    

                    if (model.ReturnUrl == null || model.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(model.ReturnUrl);

                }
                foreach (var error in result.Errors)
                {ModelState.AddModelError(string.Empty, error.Description);}
            }
            
            return View();
        }




        public IActionResult SignIn(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = new SignInModel();

            if (returnUrl != null)
                model.ReturnUrl = returnUrl;
            else
            {
                model.ReturnUrl = "/";
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, false);

                if (result.Succeeded)
                {
                    if(model.ReturnUrl == null || model.ReturnUrl =="/")
                        return RedirectToAction("Index", "Home");
                    // else
                    //     return LocalRedirect(model.ReturnUrl);
                    

                }
            }

            ModelState.AddModelError(string.Empty, "Felaktigt försök");
            ViewData["Error"] = "Felaktigt försök";

            if(_signInManager.IsSignedIn(User))
                model.DisplayName = await _profileManager.GetProfileDisplayName(model.Email);

            return View(model);
        }

        [Authorize]
        public new async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Denied()
        {
            return View();
        }

        
    }
}
