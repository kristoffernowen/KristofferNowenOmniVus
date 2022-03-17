using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Models;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;

namespace NewOmniVus.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        

    }
}
