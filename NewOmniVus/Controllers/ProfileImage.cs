using KristofferNowen_OmniVus.Models.Fileupload;
using Microsoft.AspNetCore.Mvc;
using NewOmniVus.Data;

namespace KristofferNowen_OmniVus.Controllers
{
    public class ProfileImage : Controller
    {
        private readonly SecondDbContext _secondDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileImage(SecondDbContext secondDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _secondDbContext = secondDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {



            return View();
        }

        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(ProfileImageUploadForm form)
        {

            var userId = User.FindFirst("Id").Value;

            

            if (ModelState.IsValid)
            {
                

                var profileImageEntity = new ProfileImageEntity();

                string wwwrootPath = _webHostEnvironment.WebRootPath;

                


                profileImageEntity.FileName = $"{userId}_{form.File.FileName}";
                profileImageEntity.UserId = userId;
                string filePath = Path.Combine($"{wwwrootPath}/profileImages", profileImageEntity.FileName);

                

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await form.File.CopyToAsync(fs);
                }

                _secondDbContext.ProfileImages.Add(profileImageEntity);
                await _secondDbContext.SaveChangesAsync();

                return View();
            }

            return View(form);
        }
    }
}
