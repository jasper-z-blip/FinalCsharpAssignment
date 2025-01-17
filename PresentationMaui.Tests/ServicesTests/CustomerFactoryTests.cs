using Xunit;
using Shared.Factories;

namespace PresentationMaui.Tests.ServicesTests
{
    public class CustomerFactoryTests
    {
        [Fact]
        public void AddCustomer_CustomerDataShouldBeCorrect()
        {
            //Arrange
            var customerFactory = new CustomerFactory();

            // Testdata för att skapa en kund.
            var firstName = "Jasper";
            var lastName = "Packalen";
            var email = "jasper@domain.se";
            var phoneNumber = "0701234567";
            var address = "Högtalargatan 22";
            var postalCode = "12345";
            var city = "Köping";

            //Act
            var customer = customerFactory.CreateCustomer(firstName, lastName, email, phoneNumber, address, postalCode, city);

            //Assert (Kontroll att datan för den skapade kunden är korrekt.
            Assert.NotNull(customer); // Kunden inte är null.
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phoneNumber, customer.PhoneNumber);
            Assert.Equal(address, customer.Address);
            Assert.Equal(postalCode, customer.PostalCode);
            Assert.Equal(city, customer.City);
        }
    }
}
