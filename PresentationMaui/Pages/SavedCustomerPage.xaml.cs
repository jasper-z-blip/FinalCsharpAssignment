using Shared.Interfaces;
using Shared.Models;

namespace PresentationMaui.Pages;

public partial class SavedCustomerPage : ContentPage
{
    private readonly ICustomerManagerService _customerManager;

    // Konstruktor som tar emot ICustomerManagerService, vilket är en tjänst som ansvarar för att hämta och hantera data för sparade kunder.
    public SavedCustomerPage(ICustomerManagerService customerManager)
    {
        InitializeComponent();
        _customerManager = customerManager;
    }

    // Metoden körs varje gång sidan visas för att säkerställa att kundlistan alltid är uppdaterad.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        try
        {
            // Hämtar en lista över sparade kunder med hjälp av CustomerManagerService.
            var loadedCustomers = await _customerManager.LoadCustomersAsync();
            CustomerListView.ItemsSource = loadedCustomers;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load customers: {ex.Message}", "OK");
        }
    }

    private async void OnDeleteCustomerClicked(object sender, EventArgs e)
    {
        // Hämtar den valda kunden från CommandParameter.
        if ((sender as MenuItem)?.CommandParameter is Customer selectedCustomer)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this customer?", "Yes", "No");
            if (confirm)
            {
                try
                {
                    var success = await _customerManager.DeleteCustomerAsync(selectedCustomer);
                    if (success)
                    {
                        // Om raderingen lyckades så kommer kundlistan att laddas om.
                        await LoadCustomersAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete the customer.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }
    }
}