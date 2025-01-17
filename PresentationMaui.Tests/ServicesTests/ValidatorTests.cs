using Shared.Models;
using Shared.Services;
using Xunit;

namespace PresentationMaui.Tests.ServicesTests
{
    //Testar om FirstName är tomt så returnerar Validate False.
    public class ValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnFalse_IfFirstNameIsEmpty()
        {
            //Arrange (Skapar en kund med ett tomt FirstName.)
            var customer = new Customer
            {
                FirstName = "",
                LastName = "Packalen",
                Email = "jasper@domain.se",
                PhoneNumber = "0701234567",
                Address = "Högtalargatan 22",
                PostalCode = "12345",
                City = "Köping"
            };

            var validator = new CustomerValidator();

            //Act
            var result = validator.Validate(customer, out string errorMessage );

            //Assert (Kontroll att valideringen blev misslyckad och då skrivs felmeddelandet ut.)
            Assert.False(result);
            Assert.Equal("Firstname is required.", errorMessage);
        }

        // Testar
        [Fact]
        public void Validate_ShouldReturnFalse_IfEmailIsInvalid()
        {
            //Arrange
            var customer = new Customer
            {
                FirstName = "Jasper",
                LastName = "Packalen",
                Email = "",
                PhoneNumber = "0701234567",
                Address = "Högtalargatan 22",
                PostalCode = "12345",
                City = "Köping"
            };

            var validator = new CustomerValidator();

            //Act
            var result = validator.Validate(customer, out string errorMessage );

            //Assert
            Assert.False(result);
            Assert.Equal("A valid email is required for example: jasper@domain.com", errorMessage);
        }
    }
}
