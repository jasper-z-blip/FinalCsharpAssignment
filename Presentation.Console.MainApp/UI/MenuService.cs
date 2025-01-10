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
        public void AddNewContact()
        {
            System.Console.Clear();
            System.Console.WriteLine("Add new contact");

            var contact = new Contact
            {
                FirstName = ReadValidInput("First Name", ValidationService.IsValidName, "Name must contain only letters and be at least 2 characters long."
                ),
                LastName = ReadValidInput("Last Name", ValidationService.IsValidName, "Name must contain only letters and be at least 2 characters long."
                ),
                Email = ReadValidInput("Email", ValidationService.IsValidEmail, "Email must be valid, example of valid email: jasper@domain.com"
                ),
                PhoneNumber = ReadValidInput("PhoneNumber (10 digits)", ValidationService.IsValidPhone, "Phonenumber must be exactly 10 digits and contain only numbers."
                ),
                PostalCode = ReadValidInput("Postalcode (5 digits)", ValidationService.IsValidPostalCode, "Postalcode must be exactly 5 digits and contain only numbers."
                ),
                Address = ReadNonEmptyInput("Street Adress"
                ),
                City = ReadNonEmptyInput("City")
            };

            _contactService.AddContact(contact);

            System.Console.WriteLine("Contact was successfully added! Press any key to return to the Main Menu.");
            System.Console.ReadKey();
        }

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

        private static string ReadNonEmptyInput(string fieldName)
        {
            while (true)
            {
                System.Console.Write($"{fieldName}: ");
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }

                System.Console.WriteLine($"{fieldName} can't be empty");
            }
        }

        private static string ReadValidInput(string fieldName, Func<string, bool> validationFunction, string errorMessage)
        {
            while (true)
            {
                System.Console.Write($"{fieldName}: ");
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && validationFunction(input))
                {
                    return input;
                }

                System.Console.WriteLine(errorMessage);
            }
        }
    }
}
