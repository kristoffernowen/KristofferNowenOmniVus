using NewOmniVus.Models.Addresses;

namespace NewOmniVus.Models.Profiles
{
    public class SignUpAppUserProfile
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }


        public string UserEmail { get; set; }
        public int AddressId { get; set; }


        
    }
}
