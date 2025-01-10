using Shared.Models;
using Shared.Interfaces;
using System.Collections.ObjectModel;

namespace PresentationMaui.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly ICustomerService _customerService;

        public MainPage(ICustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
        }

        private async void Button_Add_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(PostalCodeEntry.Text) ||
                string.IsNullOrWhiteSpace(CityEntry.Text))
            {
                await DisplayAlert("Validation Error", "Please fill in all fields.", "OK");
                return;
            }

            var newCustomer = new Customer
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                PhoneNumber = PhoneNumberEntry.Text,
                Email = EmailEntry.Text,
                Address = AddressEntry.Text,
                PostalCode = PostalCodeEntry.Text,
                City = CityEntry.Text,
                CustomerNumber = _customerService.GetNextCustomerNumber(await _customerService.LoadListFromJsonFile())
            };

            var customers = await _customerService.LoadListFromJsonFile();
            customers.Add(newCustomer);
            await _customerService.SaveListToJsonFile(customers);

            await DisplayAlert("Success", "Customer added successfully!", "OK");
            ClearFields();
        }

        private void ClearFields()
        {
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









