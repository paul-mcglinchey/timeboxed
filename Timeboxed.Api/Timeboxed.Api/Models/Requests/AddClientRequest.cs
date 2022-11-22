namespace Timeboxed.Api.Models.Requests
{
    public class AddClientRequest
    {
        public AddClientRequest(string firstName, string lastName, string primaryEmailAddress, string colour)
        {
            FirstName = firstName;
            LastName = lastName;
            PrimaryEmailAddress = primaryEmailAddress;
            Colour = colour;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public string Colour { get; set; }
    }
}
