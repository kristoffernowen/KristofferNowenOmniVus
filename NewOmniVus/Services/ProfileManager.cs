using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Models;
using NewOmniVus.Models.Addresses;
using NewOmniVus.Models.Profiles;

namespace NewOmniVus.Services
{
    public class ProfileManager
    {
        private readonly SecondDbContext _secondDbContext;

        public ProfileManager(SecondDbContext secondDbContext)
        {
            _secondDbContext = secondDbContext;
        }

        public async Task<IEnumerable<AppUserProfile>> GetAllProfilesAsync()
        {
            return await _secondDbContext.Profiles.ToListAsync();
        }

        public async Task<AppUserProfile> GetProfileAsync(string userEmail)
        {
            return await _secondDbContext.Profiles.SingleOrDefaultAsync(x => x.UserEmail == userEmail);
        }

        public async Task<string> CreateProfileAsync(SignUpAppUserProfile model)
        {
            

            var doesProfileExist =
                await _secondDbContext.Profiles.SingleOrDefaultAsync(x => x.UserEmail == model.UserEmail);
            if (doesProfileExist == null)
            {
                _secondDbContext.Profiles.Add(new AppUserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserEmail = model.UserEmail,
                    AddressId = model.AddressId
                });

                await _secondDbContext.SaveChangesAsync();

                // addressId = address.Id;
            }
            else
            {
                return "Profile with this email already exists";
            }

            return ("Profile created");
        }

        public async Task<string> GetProfileDisplayName(string email)
        {

            var profile = await _secondDbContext.Profiles.FirstOrDefaultAsync(x => x.UserEmail == email);

            return profile.FirstName + " " + profile.LastName; ;
        }
    }

}
