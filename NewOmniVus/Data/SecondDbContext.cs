using Microsoft.EntityFrameworkCore;
using NewOmniVus.Models;

namespace NewOmniVus.Data
{
    public class SecondDbContext : DbContext
    {
        public SecondDbContext(DbContextOptions<SecondDbContext> options) : base(options)
        {
        }
        public DbSet<SecondUser> SecondUsers { get; set; }
    }
}
