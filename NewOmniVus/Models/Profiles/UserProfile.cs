using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required, PersonalData]
        public string FirstName { get; set; }
        [Required, PersonalData]
        public string LastName { get; set; }
    }
}
