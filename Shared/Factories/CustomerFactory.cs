using Shared.Models;

namespace Shared.Factories
{
    // Skapar customer objekt.
    public class CustomerFactory
    {
        //Skapar och tar tillbaka ett nytt customer objekt.
        public Customer CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string address, string postalCode, string city)
        {
            // Skapar customer objekt med värdena som angivits.
            return new Customer
            {
                FirstName = firstName, // Tilldelar customers förnamn.
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                PostalCode = postalCode,
                City = city
            };
        }
    }
}
