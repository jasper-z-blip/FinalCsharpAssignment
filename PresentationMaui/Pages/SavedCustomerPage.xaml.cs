using Shared.Interfaces;
using Shared.Models;

namespace PresentationMaui.Pages;

public partial class SavedCustomerPage : ContentPage
{
    private readonly ICustomerManagerService _customerManager;

    // Konstruktor som tar emot ICustomerManagerService, vilket �r en tj�nst som ansvarar f�r att h�mta och hantera data f�r sparade kunder.
    public SavedCustomerPage(ICustomerManagerService customerManager)
    {
        InitializeComponent();
        _customerManager = customerManager;
    }

    // Metoden k�rs varje g�ng sidan visas f�r att s�kerst�lla att kundlistan alltid �r uppdaterad.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        try
        {
            // H�mtar en lista �ver sparade kunder med hj�lp av CustomerManagerService.
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
        // H�mtar den valda kunden fr�n CommandParameter.
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
                        // Om raderingen lyckades s� kommer kundlistan att laddas om.
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