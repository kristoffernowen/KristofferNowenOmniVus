using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NewOmniVus.Models
{
    public class SecondUser
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
    }
}
