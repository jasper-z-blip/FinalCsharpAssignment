using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shared.Models;
using Shared.Interfaces;
using Shared.Services;

// Denna kod är som en mall från chatGPT, med andra metodnamn och viss ändrad funktionalitet.
namespace PresentationMaui.ViewModels
{
    public class EditCustomerViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerService _customerService;

        // Listan över alla kunder.
        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();

        // Den kund som redigeras just nu.
        private Customer? _customerToEdit;

        // Kundnummer som användaren matar in.
        private string _customerNumber = string.Empty;

        public EditCustomerViewModel(ICustomerService customerService)
        {

            _customerService = customerService;
            LoadCustomersAsync();
        }

        // Listan över kunder som visas i applikationen.
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        // Kunden som redigeras.
        public Customer? CustomerToEdit
        {
            get => _customerToEdit;
            set
            {
                _customerToEdit = value;
                OnPropertyChanged();
            }
        }

        public string CustomerNumber
        {
            get => _customerNumber;
            set
            {
                _customerNumber = value;
                OnPropertyChanged();
            }
        }

        // Laddar alla kunder från JSON-fil.
        public async Task LoadCustomersAsync()
        {
            var loadedCustomers = await _customerService.LoadListFromJsonFile();
            Customers = new ObservableCollection<Customer>(loadedCustomers);
        }

        public async Task LoadCustomerByNumberAsync(int customerNumber)
        {
            // Ladda om listan från JSON-filen
            Customers = new ObservableCollection<Customer>(await _customerService.LoadListFromJsonFile());

            // Letar efter den första kunden i listan "Customers" vars kundnummer matchar det givna kundnumret.
            CustomerToEdit = Customers.FirstOrDefault(c => c.CustomerNumber == customerNumber);

            // Kontroll om en kund hittas.
            if (CustomerToEdit == null)
            {
                Console.WriteLine($"No customer found with number: {customerNumber}");
            }
            else
            {
                // Loggar information om den hittade kunden (förnamn och efternamn).
                Console.WriteLine($"Customer found: {CustomerToEdit.FirstName} {CustomerToEdit.LastName}");
            }
        }

        // Sparar ändringar i kundlistan.
        public async Task SaveCustomerAsync()
        {
            // Kontrollera att det finns en kund att redigera. Om ingen kund är vald, avbryt metoden.
            if (CustomerToEdit == null)
            {
                // Om CustomerToEdit är null, visa ett felmeddelande och avbryt metoden.
                await Shell.Current.DisplayAlert("Error", "No customer selected for editing.", "OK");
                return;
            }

            // Validera kunduppgifterna innan sparning.
            var validator = new CustomerValidator();
            if (!validator.Validate(CustomerToEdit, out string errorMessage))
            {
                // Om validering misslyckas, visa felmeddelande och avbryt sparning.
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "OK");
                return;
            }

            // Ladda listan med befintliga kunder från JSON-filen.
            var customers = await _customerService.LoadListFromJsonFile();

            // Hitta den existerande kunden i listan baserat på kundens CustomerNumber.
            var existingCustomer = customers.FirstOrDefault(c => c.CustomerNumber == CustomerToEdit.CustomerNumber);
            if (existingCustomer != null)
            {
                // Uppdatera den existerande kundens data med de nya uppgifterna från CustomerToEdit.
                existingCustomer.FirstName = CustomerToEdit.FirstName;
                existingCustomer.LastName = CustomerToEdit.LastName;
                existingCustomer.Email = CustomerToEdit.Email;
                existingCustomer.PhoneNumber = CustomerToEdit.PhoneNumber;
                existingCustomer.Address = CustomerToEdit.Address;
                existingCustomer.PostalCode = CustomerToEdit.PostalCode;
                existingCustomer.City = CustomerToEdit.City;

                // Spara den uppdaterade kundlistan tillbaka till JSON-filen.
                await _customerService.SaveListToJsonFile(new ObservableCollection<Customer>(customers));

                // Ladda om kundlistan från JSON-filen och uppdatera ViewModel.
                Customers = new ObservableCollection<Customer>(await _customerService.LoadListFromJsonFile());

                await Shell.Current.DisplayAlert("Success", "Customer details updated!", "OK");
            }
            else
            {
                // Om kunden inte hittades, visa ett felmeddelande
                await Shell.Current.DisplayAlert("Error", "Customer not found. Please try again.", "OK");
            }
        }

        // Tar bort en kund från listan och sparar.
        public async Task DeleteCustomerAsync()
        {
            if (CustomerToEdit != null)
            {
                var customers = await _customerService.LoadListFromJsonFile();

                // Hitta kunden att ta bort baserat på ID.
                var customerToRemove = customers.FirstOrDefault(c => c.Id == CustomerToEdit.Id);
                if (customerToRemove != null)
                {
                    // Ta bort kunden från listan.
                    customers.Remove(customerToRemove);

                    // Spara den uppdaterade listan till filen.
                    await _customerService.SaveListToJsonFile(new ObservableCollection<Customer>(customers));

                    // Uppdatera ObservableCollection för UI.
                    Customers = new ObservableCollection<Customer>(customers);

                    // Rensa bort vald kund.
                    CustomerToEdit = null;
                }
            }
        }


        // Används för att uppdatera UI när en egenskap i ViewModel ändras.
        public event PropertyChangedEventHandler? PropertyChanged;

        // OnPropertyChanged meddelar UI om att en egenskap har ändrats, så att rätt data visas.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
