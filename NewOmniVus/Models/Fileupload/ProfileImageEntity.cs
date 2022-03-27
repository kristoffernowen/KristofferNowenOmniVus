using System.ComponentModel.DataAnnotations;

namespace KristofferNowen_OmniVus.Models.Fileupload
{
    public class ProfileImageEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
