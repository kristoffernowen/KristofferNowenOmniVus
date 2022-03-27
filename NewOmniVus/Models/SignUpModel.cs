using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "You must submit a firstname")]
        [Display(Name = "First name")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{1,}$", ErrorMessage = "Must be a valid name")]
        
        public string FirstName { get; set; }
        [Required(ErrorMessage ="You must submit a lastname")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", ErrorMessage = "Must be a valid name")]

        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You must submit an email")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must submit a password")]
        [StringLength(256, ErrorMessage = "Password must contain at least 8 characters", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*\d)(?=.*[A-Z]).{8,50})", ErrorMessage = "Must be at least 8 characters, 1 uppercase letter, 1 special character, alphanumeric character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "You must confirm your password")]
        [Compare("Password", ErrorMessage = "passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        [Required]
        [Display(Name = "Address line 1")]
        [StringLength(100, ErrorMessage = "Must be at least 5 characters and at most 100 long", MinimumLength = 5)]
        public string AddressLine { get; set; }
        [Required]
        [Display(Name = "Address line 1")]
        [StringLength(100, ErrorMessage = "Must be at least 5 characters and at most 100 long", MinimumLength = 5)]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Address line 1")]
        [StringLength(100, ErrorMessage = "Must be at least 2 characters and at most 100 long", MinimumLength = 2)]
        public string City { get; set; }

        public IFormFile File { get; set; }
    }
}
