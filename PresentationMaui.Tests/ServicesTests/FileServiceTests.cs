using Shared.Services;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace PresentationMaui.Tests.ServicesTests
{
    public class FileServiceTest
    {
        // Testar metoden WriteFileAsync skapar en fil med det angivna innehållet och att det finns en fil efter att metoden har körts.
        [Fact]
        public async Task WriteFileAsync_ShouldCreateFile()
        {
            //Arrange
            var filePath = "testfile.json";
            var content = "Test Content";

            var fileService = new FileService();

            //Act
            await fileService.WriteFileAsync(filePath, content);

            //Assert
            Assert.True(File.Exists(filePath));

            File.Delete(filePath);
        }

        [Fact]
        public void FileExists_ShouldReturnTrue_WhenFileExists()
        {
            // Skapar testfil med innehållet Test Content, kollar så att det finns en fil innan metoden blir testad.
            //Arrange 
            var filePath = "testfile.json";
            File.WriteAllText(filePath, "Test Content");

            var fileService = new FileService();

            //Act
            var result = fileService.FileExists(filePath);

            //Assert
            Assert.True(result);

            File.Delete(filePath);
        }

        [Fact]
        public void FileExists_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            //Arrange
            var filePath = "nofile.json";
            var fileService = new FileService();

            //Act
            var result = fileService.FileExists(filePath);

            //Assert
            Assert.False(result);
        }
    }
}
