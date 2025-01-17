using Shared.Models;
using Shared.Interfaces;
using Shared.Factories;
using Shared.Services;

namespace PresentationMaui.Pages;

public partial class MainPage : ContentPage
{
    private readonly ICustomerManagerService _customerManagerService;
    private readonly CustomerFactory _customerFactory;

    public MainPage(ICustomerManagerService customerManagerService, CustomerFactory customerFactory)
    {
        InitializeComponent();
        _customerManagerService = customerManagerService;
        _customerFactory = customerFactory;
    }

    public async void OnSaveContactClicked(object sender, EventArgs e)
    {
        // Skapa en ny kund från inmatningsfälten.
        var newCustomer = new Customer
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Email = EmailEntry.Text,
            PhoneNumber = PhoneNumberEntry.Text,
            Address = AddressEntry.Text,
            PostalCode = PostalCodeEntry.Text,
            City = CityEntry.Text
        };

        // Validera kunden.
        var validator = new CustomerValidator();
        if (!validator.Validate(newCustomer, out string errorMessage))
        {
            // Om valideringen misslyckas, visa ett felmeddelande och returnera.
            await DisplayAlert("Validation Error", errorMessage, "OK");
            return;
        }

        // Hämtar nästa kundnummer, så varje kund får ett unikt nummer.
        var newCustomerNumber = await _customerManagerService.GetNextCustomerNumberAsync();
        newCustomer.CustomerNumber = newCustomerNumber;

        // Spara den validerade kunden.
        await _customerManagerService.AddCustomerAsync(newCustomer);

        await DisplayAlert("Success", "Customer saved successfully!", "OK");
        ClearForm();
    }


    // Rensar alla inmatningsfält i formuläret tex. när kund blivit sparad, så man kan fylla i nästa kund.
    public void ClearForm()
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