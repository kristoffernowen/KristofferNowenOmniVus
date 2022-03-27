using System.ComponentModel.DataAnnotations;

namespace NewOmniVus.Models.Profiles
{
    public class EditAppUserProfile
    {
        [Required(ErrorMessage = "You must submit a firstname")]
        [Display(Name = "First name")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{1,}$", ErrorMessage = "Must be a valid name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You must submit a lastname")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", ErrorMessage = "Must be a valid name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You must submit an email")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }
        [Display(Name = "current password")]

        [StringLength(256, ErrorMessage = "password must be at least 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "current password")]
        
        [StringLength(256, ErrorMessage = "password must be at least 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Display(Name = "new password")]
        [StringLength(256, ErrorMessage = "password must be at least 8 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = "";

        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "Address line 1")]
        [StringLength(100, ErrorMessage = "Must be at least 5 characters and at most 100 long", MinimumLength = 5)]
        public string AddressLine { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        [StringLength(100, ErrorMessage = "Must be at least 5 characters and at most 100 long", MinimumLength = 5)]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "City")]
        [StringLength(100, ErrorMessage = "Must be at least 5 characters and at most 100 long", MinimumLength = 5)]
        public string City { get; set; }

        public string ImageFileName { get; set; }
        
        public string Role { get; set; }
        
        
    }
}
