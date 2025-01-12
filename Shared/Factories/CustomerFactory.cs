using Shared.Models;

namespace Shared.Factories
{
    public class CustomerFactory
    {
        public Customer CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string address, string postalCode, string city)
        {
            return new Customer
            {
                FirstName = firstName,
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
