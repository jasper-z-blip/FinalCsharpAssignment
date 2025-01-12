using Shared.Interfaces;
using Shared.Models;

namespace PresentationMaui.Pages;

public partial class SavedCustomerPage : ContentPage
{
    private readonly ICustomerService _customerService;

    public SavedCustomerPage(ICustomerService customerService)
    {
        InitializeComponent();
        _customerService = customerService;
    }

    // En metod som hoppar igång när användaren går till sidan. Använder async för applikationen inte ska verka "frysa" när man sparar eller laddar.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCustomersAsync();
    }

    // En metod som hämtar lista med customers via LoadListFromJsonFile från ICustomerService.
    private async Task LoadCustomersAsync() 
    {
        var loadedCustomers = await _customerService.LoadListFromJsonFile();
        CustomerListView.ItemsSource = loadedCustomers;
    }

    // Metod som raderar en customer, listan ska ladddas, kunden tas bort, listan sparas om och sen ska man kunna se den uppdatera listan i SavedCustomers när man navigerar dit.
    private async void OnDeleteCustomerClicked(object sender, EventArgs e) 
    {
        if ((sender as MenuItem)?.CommandParameter is Customer selectedCustomer)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this customer?", "Yes", "No");
            if (confirm)
            {
                var customers = await _customerService.LoadListFromJsonFile();
                customers.Remove(selectedCustomer);
                await _customerService.SaveListToJsonFile(customers);
                await LoadCustomersAsync();
            }
        }
    }

    // Den här koden när helt kopierad från chatGPT, den gör detta: När anävndaren trycker på Edit så kommer den customer som man valt att hämtas via commandParameter.
    // Maui shell löser navigeringen till sidan EditCustomerPage. Id skickas.
    private async void OnEditCustomerClicked(object sender, EventArgs e)
    {
        if ((sender as MenuItem)?.CommandParameter is Customer selectedCustomer)
        {
            await Shell.Current.GoToAsync($"EditCustomerPage?CustomerId={selectedCustomer.Id}");
        }
    }
} //Koden är som mall tagen från chatGPT, men jag har ändrat metodnamnen (inte alla, vissa tyckte jag hade bra namn) och funktionaleitet för att passa med mitt projekt.