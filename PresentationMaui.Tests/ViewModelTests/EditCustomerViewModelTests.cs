using Moq;
using Shared.Interfaces;
using Shared.Models;
using PresentationMaui.ViewModels;
using System.Collections.ObjectModel;
using Xunit;

// Koden är baserad på exempel och vägledning från ChatGPT, med anpassningar.
namespace PresentationMaui.Tests.ViewModelTests
{
    public class EditCustomerViewModelTests
    {
        // Test att LoadCustomerAsync laddar alla kunder.
        [Fact]
        public async Task LoadCustomersAsync_ShouldLoadCustomers()
        {
            //Arrange 
            var mockCustomerService = new Mock<ICustomerService>();
            var customers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    CustomerNumber = 1,
                    FirstName = "Jasper",
                    LastName = "Packalen",
                    PhoneNumber = "0701234567",
                    Email = "jasper@domain.se",
                    Address = "Högtalargatan 22",
                    PostalCode = "12345",
                    City = "Köping"
                },
                new Customer
                {
                    CustomerNumber = 2,
                    FirstName = "Emily",
                    LastName = "Madden",
                    PhoneNumber = "0709876543",
                    Email = "emily@domain.se",
                    Address = "Datorvägen 11",
                    PostalCode = "67890",
                    City = "Örebro"
                }
            };

            mockCustomerService.Setup(service => service.LoadListFromJsonFile()).ReturnsAsync(customers);

            var viewModel = new EditCustomerViewModel(mockCustomerService.Object);

            //Act
            await viewModel.LoadCustomersAsync();

            //Assert
            Assert.Equal(2, viewModel.Customers.Count);
            Assert.Contains(viewModel.Customers, c => c.FirstName == "Jasper");
        }

        // Test att LoadCustomerByNumberAsync hittar en kund baserat på kundnumret kunden blev tilldelad.
        [Fact]
        public async Task LoadCustomerByNumberAsync_ShouldFindCustomer()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var customers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    CustomerNumber = 1,
                    FirstName = "Jasper",
                    LastName = "Packalen",
                    PhoneNumber = "0701234567",
                    Email = "jasper@domain.se",
                    Address = "Högtalargatan 22",
                    PostalCode = "12345",
                    City = "Köping"
                }
            };
            mockCustomerService.Setup(service => service.LoadListFromJsonFile()).ReturnsAsync(customers);

            var viewModel = new EditCustomerViewModel(mockCustomerService.Object);
            await viewModel.LoadCustomersAsync();

            //Act
            await viewModel.LoadCustomerByNumberAsync(1);

            //Assert
            Assert.NotNull(viewModel.CustomerToEdit);
            Assert.Equal("Jasper", viewModel.CustomerToEdit.FirstName);
        }

        // Test att LoadCustomerByNumberAsync returnerar null om kunden inte existerar.
        [Fact]
        public async Task LoadCustomerByNumberAsync_ShouldReturnNull_WhenCustomerNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var customers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    CustomerNumber = 1,
                    FirstName = "Jasper",
                    LastName = "Packalen",
                    PhoneNumber = "0701234567",
                    Email = "jasper@domain.se",
                    Address = "Högtalargatan 22",
                    PostalCode = "12345",
                    City = "Köping"
                }
            };
            mockCustomerService.Setup(service => service.LoadListFromJsonFile()).ReturnsAsync(customers);

            var viewModel = new EditCustomerViewModel(mockCustomerService.Object);
            await viewModel.LoadCustomersAsync();

            //Act
            await viewModel.LoadCustomerByNumberAsync(99);

            //Assert
            Assert.Null(viewModel.CustomerToEdit);
        }

        // Test att DeleteCustomerAsync tar bort en kund.
        [Fact]
        public async Task DeleteCustomerAsync_ShouldRemoveCustomer()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var customers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    CustomerNumber = 1,
                    FirstName = "Jasper",
                    LastName = "Packalen",
                    PhoneNumber = "0701234567",
                    Email = "jasper@domain.se",
                    Address = "Högtalargatan 22",
                    PostalCode = "12345",
                    City = "Köping"
                }
            };
            mockCustomerService.Setup(service => service.LoadListFromJsonFile()).ReturnsAsync(customers);
            mockCustomerService.Setup(service => service.SaveListToJsonFile(It.IsAny<ObservableCollection<Customer>>()))
                .Returns(Task.CompletedTask);

            var viewModel = new EditCustomerViewModel(mockCustomerService.Object);
            await viewModel.LoadCustomersAsync();
            await viewModel.LoadCustomerByNumberAsync(1);

            //Act
            await viewModel.DeleteCustomerAsync();

            //Assert (Komtroll att kunden blivit borttagen.)
            Assert.Empty(viewModel.Customers);
            mockCustomerService.Verify(service => service.SaveListToJsonFile(It.Is<ObservableCollection<Customer>>(c =>
                !c.Any(customer => customer.CustomerNumber == 1))), Times.Once);
        }
    }
}