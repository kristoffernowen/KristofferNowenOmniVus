using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Models
{
    public class AppAddress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [PersonalData]
        [Display(Name = "Gatuadress"), Column(TypeName = "nvarchar(50)")]
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
