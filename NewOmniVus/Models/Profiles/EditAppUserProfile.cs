using System.ComponentModel.DataAnnotations;

namespace NewOmniVus.Models.Profiles
{
    public class EditAppUserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "E-Postadress")]
        
        [EmailAddress(ErrorMessage = "E-postadressen måste vara giltig")]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        
        [StringLength(256, ErrorMessage = "Lösenordet måste bestå av minst 8 tecken", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Bekräfta Lösenord")]
        
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; } 

        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
