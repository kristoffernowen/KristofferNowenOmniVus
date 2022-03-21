using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;

namespace NewOmniVus.Services
{
    public class AddressManager
    {
        private readonly SecondDbContext _secondDbContext;

        public AddressManager(SecondDbContext secondDbContext)
        {
            _secondDbContext = secondDbContext;
        }

        public async Task<IEnumerable<AppAddressEntity>> GetAllAddressesAsync()
        {
            return await _secondDbContext.Addresses.ToListAsync();
        }

        public async Task<AppAddressEntity> GetAddressAsync(int addressId)
        {
            return await _secondDbContext.Addresses.SingleOrDefaultAsync(x => x.Id == addressId);
        }

        public async Task<int> CreateUserAddressAsync(AppAddressEntity address)
        {
            int addressId;

            var doesAddressExist = await _secondDbContext.Addresses.FirstOrDefaultAsync(x =>
                x.AddressLine == address.AddressLine && x.PostalCode == address.PostalCode && x.City == address.City);
            if (doesAddressExist == null)
            {
                _secondDbContext.Add(address);
                await _secondDbContext.SaveChangesAsync();

                addressId = address.Id;
            }
            else
            {
                addressId = doesAddressExist.Id;
            }

            return (addressId);
        }
    }

}
