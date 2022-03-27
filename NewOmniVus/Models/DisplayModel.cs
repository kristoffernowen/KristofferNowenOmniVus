namespace NewOmniVus.Models
{
    public class DisplayModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; private set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public string ImageFileName { get; set; }
        public DisplayModel()
        {
            DisplayName = FirstName + " " + LastName;
        }
        public DisplayModel(string firstName, string lastName, string userId)
        {
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
            DisplayName = FirstName + " " + LastName;
            
        }
    }
}
