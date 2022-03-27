using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KristofferNowen_OmniVus.Models.Fileupload
{
    public class ProfileImageUploadForm
    {

        
        [Display(Name = "Upload File")]
        [Required]
        public IFormFile File { get; set; }

    }
}
