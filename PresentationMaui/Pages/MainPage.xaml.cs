using Shared.Models;
using Shared.Interfaces;
using Shared.Factories;
using Shared.Services;

namespace PresentationMaui.Pages;

public partial class MainPage : ContentPage
{
    private readonly ICustomerService _customerService;
    private readonly CustomerFactory _customerFactory;

    public MainPage(ICustomerService customerService, CustomerFactory customerFactory)
    {
        InitializeComponent();
        _customerService = customerService;
        _customerFactory = customerFactory;
    }

    private async void OnSaveContactClicked(object sender, EventArgs e)
    {
        var customers = await _customerService.LoadListFromJsonFile();

        var newCustomerNumber = _customerService.GetNextCustomerNumber(customers);

        var newCustomer = new Customer
        {
            CustomerNumber = newCustomerNumber,
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Email = EmailEntry.Text,
            PhoneNumber = PhoneNumberEntry.Text,
            Address = AddressEntry.Text,
            PostalCode = PostalCodeEntry.Text,
            City = CityEntry.Text
        };

        var validator = new CustomerValidator();
        if (!validator.Validate(newCustomer, out string errorMessage))
        {
            await DisplayAlert("Validation Error", errorMessage, "OK");
            return;
        }

        await _customerService.AddCustomer(newCustomer);

        await DisplayAlert("Success", "Customer saved successfully!", "OK");
        ClearForm();
    }

    private void ClearForm()
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