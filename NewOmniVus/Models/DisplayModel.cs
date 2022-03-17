namespace NewOmniVus.Models
{
    public class DisplayModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; private set; }

        public DisplayModel(string firstName, string lastName, string userId)
        {
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
            DisplayName = FirstName + " " + LastName;
            
        }
    }
}
