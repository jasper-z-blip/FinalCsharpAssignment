using Xunit;
using Business.Models;
using Business.Services;

namespace TestsForConsolApp
{
    public class ContactTests
    {
        [Fact]
        public void AddContact_AddSuccesfully()
        {
            //Skapar temporär fil.
            string tempFilePath = "addcontact_test.json";
            var jsonService = new JsonService(tempFilePath);
            var contactService = new ContactService(jsonService);

            //Skapar den nya kontakten.
            var newContact = new Contact
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
            contactService.AddContact(newContact);
            var contacts = contactService.GetAllContacts();

            //Assert
            Assert.Single(contacts); // Kontrollera att det bara finns en kontakt.
            Assert.Equal(newContact.FirstName, contacts[0].FirstName);
            Assert.Equal(newContact.LastName, contacts[0].LastName);
            Assert.Equal(newContact.Email, contacts[0].Email);
            Assert.Equal(newContact.PhoneNumber, contacts[0].PhoneNumber);
            Assert.Equal(newContact.Address, contacts[0].Address);
            Assert.Equal(newContact.PostalCode, contacts[0].PostalCode);
            Assert.Equal(newContact.City, contacts[0].City);

            // Raderar temporära filen.
            File.Delete(tempFilePath);
        }
    }
}
