using Moq;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;
using Xunit;

// Koden är baserad på exempel och vägledning från ChatGPT, med anpassningar.
namespace Shared.Tests.Services
{
    public class CustomerManagerServiceTests
    {
        // Test att LoadCustomersAsync hämtar en lista med kunder.
        [Fact]
        public async Task LoadCustomersAsync_ShouldReturnCustomers()
        {
            // Arrange (Förberedelse mock och testdata.)
            var mockCustomerService = new Mock<ICustomerService>();
            var mockCustomers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
                    CustomerNumber = 2,
                    FirstName = "Emily",
                    LastName = "Madden",
                    PhoneNumber = "0987654321",
                    Email = "emily@domain.se",
                    Address = "Datorgatan 11",
                    PostalCode = "67890",
                    City = "Örebro"
                }
            };

            mockCustomerService
                .Setup(service => service.LoadListFromJsonFile())
                .ReturnsAsync(mockCustomers);

            var customerManager = new CustomerManagerService(mockCustomerService.Object);

            //Act
            var result = await customerManager.LoadCustomersAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.FirstName == "Jasper");
        }

        // Test att DeleteCustomerAsync tar bort en kund.
        [Fact]
        public async Task DeleteCustomerAsync_ShouldRemoveCustomer()
        {
            //Arrange (Förberedlse mock och testdata.)
            var mockCustomerService = new Mock<ICustomerService>();
            var mockCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                CustomerNumber = 1,
                FirstName = "Jasper",
                LastName = "Packalen",
                PhoneNumber = "0701234567",
                Email = "jasper@domain.se",
                Address = "Högtalargatan 22",
                PostalCode = "12345",
                City = "Köping"
            };

            var mockCustomers = new ObservableCollection<Customer> { mockCustomer };

            mockCustomerService
                .Setup(service => service.LoadListFromJsonFile())
                .ReturnsAsync(mockCustomers);

            mockCustomerService
                .Setup(service => service.SaveListToJsonFile(It.IsAny<ObservableCollection<Customer>>()))
                .Returns(Task.CompletedTask);

            var customerManager = new CustomerManagerService(mockCustomerService.Object);

            //Act
            var success = await customerManager.DeleteCustomerAsync(mockCustomer);

            //Assert (Kontroll att kunden togs bort.)
            Assert.True(success, "The customer should have been deleted successfully.");
            mockCustomerService.Verify(service => service.SaveListToJsonFile(It.Is<ObservableCollection<Customer>>(c => !c.Contains(mockCustomer))), Times.Once);
        }

        // Test att AddCustomerAsync lägger till en kund.
        [Fact]
        public async Task AddCustomerAsync_ShouldAddCustomerAndSaveList()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var customers = new ObservableCollection<Customer>();
            mockCustomerService.Setup(service => service.LoadListFromJsonFile()).ReturnsAsync(customers);
            mockCustomerService.Setup(service => service.SaveListToJsonFile(It.IsAny<ObservableCollection<Customer>>()))
                .Returns(Task.CompletedTask);

            var customerManager = new CustomerManagerService(mockCustomerService.Object);
            var newCustomer = new Customer
            {
                FirstName = "Jasper",
                LastName = "Packalen",
                Email = "jasper@domain.se",
                PhoneNumber = "0701234567",
                Address = "Högtalargatan 22",
                PostalCode = "12345",
                City = "Köping"
            };

            //Act
            await customerManager.AddCustomerAsync(newCustomer);

            // Assert (Kontroll att kunden lades till.)
            mockCustomerService.Verify(service => service.SaveListToJsonFile(It.Is<ObservableCollection<Customer>>(list => list.Contains(newCustomer))), Times.Once);
            Assert.Contains(newCustomer, customers);
        }

        // Test att UpdateCustomerAsync uppdaterar en kund.
        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdateExistingCustomer()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var existingCustomer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Jasper",
                LastName = "Packalen",
                Email = "jasper@domain.se",
                PhoneNumber = "0701234567",
                Address = "Högtalargatan 22",
                PostalCode = "12345",
                City = "Köping"
            };
            var customers = new ObservableCollection<Customer> { existingCustomer };
            mockCustomerService.Setup(service => service.LoadListFromJsonFile()).ReturnsAsync(customers);
            mockCustomerService.Setup(service => service.SaveListToJsonFile(It.IsAny<ObservableCollection<Customer>>()))
                .Returns(Task.CompletedTask);

            var customerManager = new CustomerManagerService(mockCustomerService.Object);
            var updatedCustomer = new Customer
            {
                Id = existingCustomer.Id,
                FirstName = "Emily",
                LastName = "Madden",
                Email = "emily@domain.se",
                PhoneNumber = "0712345678",
                Address = "Datorgatan 11",
                PostalCode = "67890",
                City = "Örebro"
            };

            //Act
            var result = await customerManager.UpdateCustomerAsync(updatedCustomer);

            // Assert (Kontroll att kunden uppdaterades.)
            Assert.True(result);
            Assert.Equal("Emily", existingCustomer.FirstName);
            Assert.Equal("Madden", existingCustomer.LastName);
            Assert.Equal("emily@domain.se", existingCustomer.Email);
            mockCustomerService.Verify(service => service.SaveListToJsonFile(customers), Times.Once);
        }

        [Fact]
        public async Task GetNextCustomerNumberAsync_ShouldReturnNextNumber()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            var mockCustomers = new ObservableCollection<Customer>
        {
            new Customer
            {
                CustomerNumber = 1,
                FirstName = "Jasper",
                LastName = "Packalen",
                Email = "jasper@domain.se",
                PhoneNumber = "0701234567",
                Address = "Högtalargatan 22",
                PostalCode = "12345",
                City = "Köping"
            },
            new Customer
            {
                CustomerNumber = 2,
                FirstName = "Emily",
                LastName = "Madden",
                Email = "emily@domain.se",
                PhoneNumber = "0712345678",
                Address = "Datorgatan 11",
                PostalCode = "67890",
                City = "Örebro"
            }
        };

        mockCustomerService
            .Setup(cs => cs.LoadListFromJsonFile())
            .ReturnsAsync(mockCustomers);

        var customerManagerService = new CustomerManagerService(mockCustomerService.Object);

        //Act
        var nextNumber = await customerManagerService.GetNextCustomerNumberAsync();

        //Assert
        Assert.Equal(3, nextNumber);
        }


    }
}
