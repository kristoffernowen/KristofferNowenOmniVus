using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models;

namespace NewOmniVus.Services
{
    public class AddressManager
    {
        private readonly AppDbContext _appDbContext;

        public AddressManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<AppAddress>> GetAllAddresses()
        {
            return await _appDbContext.Addresses.ToListAsync();
        }

        // public async Task<AppAddress> GetAddress(string userId)
        // {
        //     var address = await _appDbContext.UserAddresses.Include(x => x.Address)
        //         .FirstOrDefaultAsync(x => x.UserId == userId);
        //
        //     return address.Address;
        // }

        // public async Task CreateUserAddress(IdentityUser user, AppAddress address)
        // {
        //     _appDbContext.Addresses.Add(address);
        //     await _appDbContext.SaveChangesAsync();
        //
        //     var userAddress = new AppUserAddress
        //     {
        //         UserId = user.Id,
        //         AddressId = address.Id
        //     };
        //     _appDbContext.UserAddresses.Add(userAddress);
        //     await _appDbContext.SaveChangesAsync();
        // }
    }

}
