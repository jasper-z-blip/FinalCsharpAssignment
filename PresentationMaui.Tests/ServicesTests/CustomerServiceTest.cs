using Moq;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;
using Xunit;

namespace PresentationMaui.Tests.ServicesTest
{
    public class CustomerServiceTests
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _customerService = new CustomerService(_fileServiceMock.Object);
        }

        [Fact]
        public async Task SaveListToJsonFile_ShouldCallWriteFileAsync()
        {
            // Arrange
            var customers = new ObservableCollection<Customer>
            {
                new Customer { CustomerNumber = 1, FirstName = "John", LastName = "Doe" }
            };

            string expectedJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            // Act
            await _customerService.SaveListToJsonFile(customers);

            // Assert
            _fileServiceMock.Verify(
                x => x.WriteFileAsync(It.IsAny<string>(), expectedJson),
                Times.Once
            );
        }

        [Fact]
        public async Task LoadListFromJsonFile_ShouldReturnCustomerList()
        {
            // Arrange
            var customers = new ObservableCollection<Customer>
            {
                new Customer { CustomerNumber = 1, FirstName = "John", LastName = "Doe" }
            };
            string json = JsonConvert.SerializeObject(customers);

            // Mock FileExists to return true
            _fileServiceMock.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);

            // Mock ReadFileAsync to return JSON
            _fileServiceMock.Setup(x => x.ReadFileAsync(It.IsAny<string>())).ReturnsAsync(json);

            // Act
            var result = await _customerService.LoadListFromJsonFile();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Equal("John", result[0].FirstName);
        }

        [Fact]
        public void GetNextCustomerNumber_ShouldReturnNextNumber()
        {
            // Arrange
            var customers = new ObservableCollection<Customer>
            {
                new Customer { CustomerNumber = 1 },
                new Customer { CustomerNumber = 2 }
            };

            // Act
            var nextNumber = _customerService.GetNextCustomerNumber(customers);

            // Assert
            Assert.Equal(3, nextNumber);
        }
    }
}

