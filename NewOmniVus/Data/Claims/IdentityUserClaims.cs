using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace NewOmniVus.Data.Claims
{
    public class IdentityUserClaims : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        private readonly SecondDbContext _secondDbContext;
        private readonly AppDbContext _appDbContext;
        public IdentityUserClaims(AppDbContext appDbContext, SecondDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _appDbContext = appDbContext;
            _secondDbContext = context;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            var profile = await _secondDbContext.Profiles.SingleOrDefaultAsync(x => x.UserEmail == user.Email);

            var _user = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email == user.Email);
            var _userId = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Id == _user.Id);
            var _userRole = await _appDbContext.UserRoles.SingleOrDefaultAsync(x => x.UserId == _userId.Id);
            var _role = await _appDbContext.Roles.SingleOrDefaultAsync(x => x.Id == _userRole.RoleId);

            var address = await _secondDbContext.Profiles.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.UserEmail == user.Email);
            

            claimsIdentity.AddClaim(new Claim("DisplayName", $"{profile.FirstName} {profile.LastName}"));
            claimsIdentity.AddClaim(new Claim("Role", $"{_role.Name}"));
            
            claimsIdentity.AddClaim(new Claim("Id", $"{user.Id}"));


            return claimsIdentity;
        }

        
    }
}
