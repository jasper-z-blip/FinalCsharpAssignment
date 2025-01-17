using Business.Models;
using Business.Services;

namespace Presentation.Console.MainApp.UI // Fick problem när jag använde Console i namnet så la till System framför alla Console i koden och då fungerade det.
{
    public class MenuService
    {
        private readonly ContactService _contactService;

        public MenuService(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("ContactApp - Main Menu");
                System.Console.WriteLine("1. Add new contact");
                System.Console.WriteLine("2. List of all contacts");
                System.Console.WriteLine("3. Exit program");

                string choice = System.Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        AddNewContact();
                        break;

                    case "2":
                        ListOfAllContacts();
                        break;

                    case "3":
                        System.Console.WriteLine("Closing program..");
                        Environment.Exit(0);
                        break;

                    default:
                        System.Console.WriteLine("Invalid option, press any key to try again");
                        System.Console.ReadKey();
                        break;
                }
            }
        }

        // Metod som Lägger till en ny kontakt, kollar också att valideringen stämmer.
        public void AddNewContact()
        {
            System.Console.Clear();
            System.Console.WriteLine("Add new contact");

            var contact = new Contact
            {
                FirstName = InputService.ReadValidInput("First Name", ValidationService.IsValidName, "Name must contain only letters and be at least 2 characters long."
                ),
                LastName = InputService.ReadValidInput("Last Name", ValidationService.IsValidName, "Name must contain only letters and be at least 2 characters long."
                ),
                Email = InputService.ReadValidInput("Email", ValidationService.IsValidEmail, "Email must be valid, example of valid email: jasper@domain.com"
                ),
                PhoneNumber = InputService.ReadValidInput("PhoneNumber (10 digits)", ValidationService.IsValidPhone, "Phonenumber must be exactly 10 digits and contain only numbers."
                ),
                PostalCode = InputService.ReadValidInput("Postalcode (5 digits)", ValidationService.IsValidPostalCode, "Postalcode must be exactly 5 digits and contain only numbers."
                ),
                Address = InputService.ReadNonEmptyInput("Street Adress"
                ),
                City = InputService.ReadNonEmptyInput("City")
            };

            _contactService.AddContact(contact);

            System.Console.WriteLine("Contact was successfully added! Press any key to return to the Main Menu.");
            System.Console.ReadKey();
        }


        // Metod som hanterar om inga kontakter finns och annars visar listan över kontakter.
        public void ListOfAllContacts()
        {
            System.Console.Clear();
            System.Console.WriteLine("List of All Contacts:");

            var contacts = _contactService.GetAllContacts();

            if (contacts.Count == 0)
            {
                System.Console.WriteLine("No contacts have been added");
            }
            else
            {
                int counter = 1;
                foreach (var contact in contacts)
                {
                    System.Console.WriteLine($"{counter}. {contact}");
                    System.Console.WriteLine(new string('-', 41));
                    counter++;
                }
            }

            System.Console.WriteLine("Press any key to return to the Main Menu");
            System.Console.ReadKey();
        }
    }
}
