using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Models
{
    public class AppUserAddress
    {
        public string UserId { get; set; }
        public int AddressId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual AppAddress Address { get; set; }
    }
}
