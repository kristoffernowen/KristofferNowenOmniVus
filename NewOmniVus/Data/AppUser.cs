using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Data
{
    public class AppUser : IdentityUser
    {
        [Required, PersonalData]
        public string FirstName { get; set; }
        [Required, PersonalData]

        public string LastName { get; set; }
    }
}
