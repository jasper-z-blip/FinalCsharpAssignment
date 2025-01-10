using Shared.Models;
using Shared.Interfaces;
using System.Collections.ObjectModel;

namespace PresentationMaui.Pages
{
    public partial class EditCustomerPage : ContentPage
    {
        private readonly ICustomerService _customerService;
        private ObservableCollection<Customer> _customers;
        private Customer _customerToEdit;

        public EditCustomerPage(ICustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
            _customers = new ObservableCollection<Customer>();
            _ = LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            var loadedCustomers = await _customerService.LoadListFromJsonFile();
            _customers = new ObservableCollection<Customer>(loadedCustomers);
        }

        private void EnableFields(bool isEnabled)
        {
            FirstNameEntry.IsEnabled = isEnabled;
            LastNameEntry.IsEnabled = isEnabled;
            EmailEntry.IsEnabled = isEnabled;
            PhoneNumberEntry.IsEnabled = isEnabled;
            AddressEntry.IsEnabled = isEnabled;
            PostalCodeEntry.IsEnabled = isEnabled;
            CityEntry.IsEnabled = isEnabled;
            SaveButton.IsEnabled = isEnabled;
            DeleteButton.IsEnabled = isEnabled;
        }

        private async void LoadCustomerButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(CustomerNumberEntry.Text, out int customerNumber))
            {
                _customerToEdit = _customers.FirstOrDefault(c => c.CustomerNumber == customerNumber);
                if (_customerToEdit != null)
                {
                    FirstNameEntry.Text = _customerToEdit.FirstName;
                    LastNameEntry.Text = _customerToEdit.LastName;
                    EmailEntry.Text = _customerToEdit.Email;
                    PhoneNumberEntry.Text = _customerToEdit.PhoneNumber;
                    AddressEntry.Text = _customerToEdit.Address;
                    PostalCodeEntry.Text = _customerToEdit.PostalCode;
                    CityEntry.Text = _customerToEdit.City;

                    EnableFields(true);
                }
                else
                {
                    await DisplayAlert("Error", "Customer not found.", "OK");
                    EnableFields(false);
                }
            }
            else
            {
                await DisplayAlert("Validation Error", "Please enter a valid customer number.", "OK");
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (_customerToEdit != null)
            {
                _customerToEdit.FirstName = FirstNameEntry.Text;
                _customerToEdit.LastName = LastNameEntry.Text;
                _customerToEdit.Email = EmailEntry.Text;
                _customerToEdit.PhoneNumber = PhoneNumberEntry.Text;
                _customerToEdit.Address = AddressEntry.Text;
                _customerToEdit.PostalCode = PostalCodeEntry.Text;
                _customerToEdit.City = CityEntry.Text;

                await _customerService.SaveListToJsonFile(_customers);
                await DisplayAlert("Success", "Customer details updated!", "OK");

                ClearFields();
                EnableFields(false);
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (_customerToEdit != null)
            {
                bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this customer?", "Yes", "No");
                if (confirm)
                {
                    _customers.Remove(_customerToEdit);
                    await _customerService.SaveListToJsonFile(_customers);

                    await DisplayAlert("Success", "Customer deleted successfully!", "OK");

                    ClearFields();
                    EnableFields(false);
                }
            }
            else
            {
                await DisplayAlert("Error", "No customer selected to delete.", "OK");
            }
        }

        private void ClearFields()
        {
            CustomerNumberEntry.Text = string.Empty;
            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PhoneNumberEntry.Text = string.Empty;
            AddressEntry.Text = string.Empty;
            PostalCodeEntry.Text = string.Empty;
            CityEntry.Text = string.Empty;
        }
    }
}






