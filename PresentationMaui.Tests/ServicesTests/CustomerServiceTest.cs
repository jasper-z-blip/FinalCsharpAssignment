using Moq;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;
using Xunit;

public class CustomerServiceTests
{
    [Fact]
    public async Task AddCustomer_ShouldAddCustomerToList()
    {
        //Arrange (Förebereder mock och skapa CustomerService.)
        var mockFileService = new Mock<IFileService>();
        var customerService = new CustomerService(mockFileService.Object);

        //Skapar en testkund.
        var customer = new Customer
        {
            CustomerNumber = 1,
            FirstName = "Jasper",
            LastName = "Packalen",
            Email = "jasper@domain.se",
            PhoneNumber = "0701234567",
            Address = "Högtalargatan 22",
            PostalCode = "12345",
            City = "Köping"
        };

        //Act
        await customerService.AddCustomer(customer);

        //Assert (Kontroll att kunden lades till korrekt)
        Assert.Single(customerService.Customers); // Att de var en kund.
        Assert.Equal("Jasper", customerService.Customers[0].FirstName); // Att kundens Firstname var Jasper.
    }

    [Fact]
    public async Task LoadListFromJsonFile_ShouldReturnEmpty_IfNoFileExists()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(f => f.FileExists(It.IsAny<string>())).Returns(false);

        var customerService = new CustomerService(mockFileService.Object);

        //Act
        var customers = await customerService.LoadListFromJsonFile();

        //Assert
        Assert.Empty(customers);
    }

    [Fact]
    public async Task RemoveCustomer_ShouldRemoveCustomerAndSaveList()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        var customerService = new CustomerService(mockFileService.Object);

        var customerToRemove = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerNumber = 1,
            FirstName = "Jasper",
            LastName = "Packalen",
            Email = "jasper@domain.se",
            PhoneNumber = "0701234567",
            Address = "Högtalargatan 22",
            PostalCode = "12345",
            City = "Köping"
        };

        customerService.Customers.Add(customerToRemove);

        mockFileService.Setup(f => f.WriteFileAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        //Act
        await customerService.RemoveCustomer(customerToRemove);

        //Assert
        Assert.Empty(customerService.Customers);
    }
}