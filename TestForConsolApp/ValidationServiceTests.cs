using Xunit;
using Business.Services;

namespace TestsForConsolApp
{
    public class ValidationServiceTests
    {
        [Fact]
        public void IsValidName_validNames_ReturnsTrue()
        {
            //Arrange
            var validNames = new[] { "Jasper", "Jasper-Adde", "Jasper Adde" };

            foreach (var name in validNames)
            { 
                //Act
                var result = ValidationService.IsValidName(name);

                //Assert
                Assert.True(result, $"Expected '{name}' to be a valid name. ");
            }
        }

        [Fact]
        public void IsValidName_invalidNames_ReturnsFalse()
        {
            //Arrange
            var invalidNames = new[] { "Jasper.Adde", "Jasper,Adde", "J", "1234", "", "Jasper$" };

            foreach(var name in invalidNames)
            {
                //Act
                var result = ValidationService.IsValidName(name);

                //Assert
                Assert.False(result, $"Expected '{name}' to be an invalid name.");
            }
        }

        [Fact]
        public void IsValidEmail_validEmails_ReturnsTrue()
        {
            //Arrange
            var emails = new[] { "jasper@domain.com", "jasper.adde@domain.se", "jasper_adde@domain.com" };

            foreach (var email in emails)
            {
                //Act
                var result = ValidationService.IsValidEmail(email);

                //Assert
                Assert.True(result, $"Expected '{email}' to be a valid email.");
            }
        }

        [Fact]
        public void IsValidEmail_invalidEmails_ReturnsFalse()
        {
            //Arrange
            var invalidEmails = new[] { "jasper@", "domain.se", "jasper@domain" };

            foreach(var email in invalidEmails)
            {
                //Act
                var result = ValidationService.IsValidEmail(email);

                //Assert
                Assert.False(result, $"Expected '{email}' to be an invalid email");
            }
        }
    }
}
