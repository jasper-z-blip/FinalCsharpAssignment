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

            // Detta laddar befintliga kontakter sparade i Json.
            _contacts = _jsonService.LoadData<Contact>();
        }
        

        // Detta lägger till ny kontakt och sparar till fil.
        public void AddContact(Contact contact) 
        {
            _contacts.Add(contact);
            _jsonService.SaveData(_contacts);
        }


        // Retunerar alla inlästa kontakter.
        public List<Contact> GetAllContacts()
        {
            return _contacts;
        }
    }
}