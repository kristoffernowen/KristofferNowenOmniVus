using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Models;

namespace NewOmniVus.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AppAddress> Addresses { get; set; }
        public DbSet<AppUserAddress> UserAddresses { get; set; }

        public DbSet<AppUserProfile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUserAddress>()
                .HasKey(c => new { c.UserId, c.AddressId });

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey("LoginProvider", "ProviderKey");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey("UserId", "RoleId");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey("UserId", "LoginProvider", "Name");

        }
    }
}
