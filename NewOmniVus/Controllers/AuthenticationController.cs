using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NewOmniVus.Data;
using NewOmniVus.Models;

namespace NewOmniVus.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        private readonly AppDbContext _appDbContext;


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
