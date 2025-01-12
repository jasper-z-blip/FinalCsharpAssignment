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

    // En metod som hoppar ig�ng n�r anv�ndaren g�r till sidan. Anv�nder async f�r applikationen inte ska verka "frysa" n�r man sparar eller laddar.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCustomersAsync();
    }

    // En metod som h�mtar lista med customers via LoadListFromJsonFile fr�n ICustomerService.
    private async Task LoadCustomersAsync() 
    {
        var loadedCustomers = await _customerService.LoadListFromJsonFile();
        CustomerListView.ItemsSource = loadedCustomers;
    }

    // Metod som raderar en customer, listan ska ladddas, kunden tas bort, listan sparas om och sen ska man kunna se den uppdatera listan i SavedCustomers n�r man navigerar dit.
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

    // Den h�r koden n�r helt kopierad fr�n chatGPT, den g�r detta: N�r an�vndaren trycker p� Edit s� kommer den customer som man valt att h�mtas via commandParameter.
    // Maui shell l�ser navigeringen till sidan EditCustomerPage. Id skickas.
    private async void OnEditCustomerClicked(object sender, EventArgs e)
    {
        if ((sender as MenuItem)?.CommandParameter is Customer selectedCustomer)
        {
            await Shell.Current.GoToAsync($"EditCustomerPage?CustomerId={selectedCustomer.Id}");
        }
    }
} //Koden �r som mall tagen fr�n chatGPT, men jag har �ndrat metodnamnen (inte alla, vissa tyckte jag hade bra namn) och funktionaleitet f�r att passa med mitt projekt.