using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using NewOmniVus.Models.Addresses;

namespace NewOmniVus.Models.Profiles
{
    public class AppUserProfileEntity
    {
        [Key]
        public string Id { get; set; }

        
        
        


        [Required, PersonalData]
        public string FirstName { get; set; }
        [Required, PersonalData]
        public string LastName { get; set; }

        
        public string UserEmail { get; set; }
        public AppAddressEntity Address { get; set; }
        public int AddressId { get; set; }
    }
}
