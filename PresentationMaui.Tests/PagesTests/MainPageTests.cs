using Moq;
using Shared.Interfaces;
using Shared.Models;
using Shared.Factories;
using Xunit;
using System.Collections.ObjectModel;
using Shared.Services;

namespace PresentationMaui.Tests.PagesTests
{
    public class MainPageTests
    {
        [Fact]
        public async Task OnSaveContactClicked_ShouldNotSaveInvalidCustomer()
        {
            //Arrange
            var customerManagerServiceMock = new Mock<ICustomerManagerService>();
            var customerFactory = new CustomerFactory();

            customerManagerServiceMock
                .Setup(service => service.LoadCustomersAsync())
                .ReturnsAsync(new ObservableCollection<Customer>());

            var page = new MockMainPage(customerManagerServiceMock.Object, customerFactory);

            page.FirstName = ""; // Tomt fält för att simulera ogiltig kund.
            page.LastName = "Madden";
            page.Email = "emily@domain.se";
            page.PhoneNumber = "0712345678";
            page.Address = "Datorgatan 11";
            page.PostalCode = "67890";
            page.City = "Örebro";

            //Act
            await page.SaveCustomer();

            //Assert
            customerManagerServiceMock.Verify(service => service.AddCustomerAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public void ClearForm_ShouldClearAllFields()
        {
            //Arrange
            var customerManagerServiceMock = new Mock<ICustomerManagerService>();
            var customerFactory = new CustomerFactory();
            var page = new MockMainPage(customerManagerServiceMock.Object, customerFactory);

            page.FirstName = "Jasper";
            page.LastName = "Packalen";
            page.Email = "jasper@domain.se";
            page.PhoneNumber = "0701234567";
            page.Address = "Högtalargatan 22";
            page.PostalCode = "12345";
            page.City = "Köping";

            //Act
            page.ClearForm();

            //Assert
            Assert.Empty(page.FirstName);
            Assert.Empty(page.LastName);
            Assert.Empty(page.Email);
            Assert.Empty(page.PhoneNumber);
            Assert.Empty(page.Address);
            Assert.Empty(page.PostalCode);
            Assert.Empty(page.City);
        }
    }

    public class MockMainPage
    {
        private readonly ICustomerManagerService _customerManagerService;
        private readonly CustomerFactory _customerFactory;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public MockMainPage(ICustomerManagerService customerManagerService, CustomerFactory customerFactory)
        {
            _customerManagerService = customerManagerService;
            _customerFactory = customerFactory;
        }

        public async Task SaveCustomer()
        {
            var customers = await _customerManagerService.LoadCustomersAsync();
            var newCustomerNumber = await _customerManagerService.GetNextCustomerNumberAsync();

            var newCustomer = new Customer
            {
                CustomerNumber = newCustomerNumber,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Address = Address,
                PostalCode = PostalCode,
                City = City
            };

            // Använder CustomerValidator för att validera kunden.
            var validator = new CustomerValidator();
            if (!validator.Validate(newCustomer, out string errorMessage))
            {
                Console.WriteLine($"Validation failed: {errorMessage}");
                return; // Returnera om valideringen misslyckades.
            }


            // Om valideringen lyckades, läggs kunden till.
            await _customerManagerService.AddCustomerAsync(newCustomer);
            ClearForm();
        }

        public void ClearForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Address = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
        }
    }
}
