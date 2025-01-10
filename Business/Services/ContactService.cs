using System.Collections.Generic;
using Business.Models;

namespace Business.Services
{
    public class ContactService
    {
        private readonly JsonService _jsonService;
        private readonly List<Contact> _contacts;

        public ContactService(JsonService jsonService)
        {
            _jsonService = jsonService;

            _contacts = _jsonService.LoadData<Contact>(); // Detta laddar befintliga kontakter sparade i Json.
        }

        public void AddContact(Contact contact) //Detta läger till ny kontakt och sparar till fil.
        {
            _contacts.Add(contact);
            _jsonService.SaveData(_contacts);
        }

        public List<Contact> GetAllContacts()
        {
            return _contacts;
        }
    }
}