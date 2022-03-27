using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewOmniVus.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "You must submit a firstname")]
        [Display(Name = "FirstName")]
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "E-Postadress")]
        [Required(ErrorMessage = "Du måste ange en e-postadress")]
        [EmailAddress(ErrorMessage = "E-postadressen måste vara giltig")]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [StringLength(256, ErrorMessage = "Lösenordet måste bestå av minst 8 tecken", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Bekräfta Lösenord")]
        [Required(ErrorMessage = "Du måste ange bekräfta lösenordet")]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }

        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public IFormFile File { get; set; }
    }
}
