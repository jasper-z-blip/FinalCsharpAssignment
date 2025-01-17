using Xunit;
using Business.Services;
using Business.Models;

namespace TestsForConsolApp
{
    public class JSonSerivceTests
    {
        [Fact]
        public void LoadingData_IfFileIsEmpty_ShouldReturnEmptyList()
        {
            //Arrange
            string tempFilePath = "empty_test.json";
            File.WriteAllText(tempFilePath, ""); //Skapar en tom fil för att testa hur JsonService hanterar en fil utan innehåll.
            var jsonService = new JsonService(tempFilePath);

            //Act (Försöker alssa data från filen.)
            var loadedData = jsonService.LoadData<Contact>();

            //Assert (Kontroll ej tom/null lista.)
            Assert.NotNull(loadedData);
            Assert.Empty(loadedData);

            File.Delete(tempFilePath); //Raderar temporära filen.
        }

        [Fact]
        public void LoadingAndSavingData_ReturnsSuccesfully()
        {
            string tempFilePath = "loadandsave_test.json";
            var jsonService = new JsonService(tempFilePath);

            // Skapar en lista med exempelkontakter med all customerinfo.
            var contactsToSave = new List<Contact>
            {
                new Contact
                { 
                    FirstName = "Jasper", 
                    LastName = "Packalen", 
                    Email = "jasper@domain.se",
                    PhoneNumber = "0701234567",
                    Address = "Högtalargatan 22",
                    PostalCode = "12345",
                    City = "Köping"
                },
                new Contact 
                { 
                    FirstName = "Emily", 
                    LastName = "Madden", 
                    Email = "email@domain.se",
                    PhoneNumber = "0704567890",
                    Address = "Datorvägen 44",
                    PostalCode = "54321",
                    City = "Örebro"
                }
            };

            //Act (Sparar listan och läser tillbaka från den temporära filen där den sparats.)
            jsonService.SaveData(contactsToSave);
            var loadedContacts = jsonService.LoadData<Contact>();

            //Assert (Kontroll på att kontakternas info är korrekt, [0] för första testkontakten, [1] för andra testkontakten.)
            Assert.Equal(contactsToSave.Count, loadedContacts.Count);
            Assert.Equal(contactsToSave[0].FirstName, loadedContacts[0].FirstName);
            Assert.Equal(contactsToSave[0].LastName, loadedContacts[0].LastName);
            Assert.Equal(contactsToSave[0].Email, loadedContacts[0].Email);
            Assert.Equal(contactsToSave[0].PhoneNumber, loadedContacts[0].PhoneNumber);
            Assert.Equal(contactsToSave[0].Address, loadedContacts[0].Address);
            Assert.Equal(contactsToSave[0].PostalCode, loadedContacts[0].PostalCode);
            Assert.Equal(contactsToSave[0].City, loadedContacts[0].City);

            Assert.Equal(contactsToSave[1].FirstName, loadedContacts[1].FirstName);
            Assert.Equal(contactsToSave[1].LastName, loadedContacts[1].LastName);
            Assert.Equal(contactsToSave[1].Email, loadedContacts[1].Email);
            Assert.Equal(contactsToSave[1].PhoneNumber, loadedContacts[1].PhoneNumber);
            Assert.Equal(contactsToSave[1].Address, loadedContacts[1].Address);
            Assert.Equal(contactsToSave[1].PostalCode, loadedContacts[1].PostalCode);
            Assert.Equal(contactsToSave[1].City, loadedContacts[1].City);

            // Raderar temporära filen.
            File.Delete(tempFilePath);
        }
    }
}
