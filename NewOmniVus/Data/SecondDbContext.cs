using Microsoft.EntityFrameworkCore;
using NewOmniVus.Models;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;

namespace NewOmniVus.Data
{
    public class SecondDbContext : DbContext
    {
        public SecondDbContext(DbContextOptions<SecondDbContext> options) : base(options)
        {
        }
        // public DbSet<SecondUser> SecondUsers { get; set; }

        public DbSet<AppAddressEntity> Addresses { get; set; }
        // public DbSet<AppUserAddress> UserAddresses { get; set; }

        public DbSet<AppUserProfileEntity> Profiles { get; set; }
        // public DbSet<AppUserAddress> UserAddresses { get; set; }

        public DbSet<NewOmniVus.Models.Profiles.SignUpAppUserProfile> SignUpAppUserProfile { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<AppUserProfileEntity>()
        //         .HasKey(c => new { c.UserEmail, c.AddressId });
        //
        //     // modelBuilder.Entity<IdentityUserLogin<string>>()
        //     //     .HasKey("LoginProvider", "ProviderKey");
        //     //
        //     // modelBuilder.Entity<IdentityUserRole<string>>()
        //     //     .HasKey("UserId", "RoleId");
        //     //
        //     // modelBuilder.Entity<IdentityUserToken<string>>()
        //     //     .HasKey("UserId", "LoginProvider", "Name");
        //
        // }
    }
}
