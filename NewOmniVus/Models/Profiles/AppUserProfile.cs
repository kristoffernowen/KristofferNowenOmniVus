using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Models
{
    public class AppUserProfile
    {
        // [Key]
        // public int Id { get; set; }
        [Required, PersonalData]
        public string FirstName { get; set; }
        [Required, PersonalData]
        public string LastName { get; set; }

        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public AppAddress Address { get; set; }
        public int AddressId { get; set; }
    }
}
