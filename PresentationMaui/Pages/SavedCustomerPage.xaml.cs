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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCustomersAsync();
    }

    private async Task LoadCustomersAsync()
    {
        var loadedCustomers = await _customerService.LoadListFromJsonFile();
        CustomerListView.ItemsSource = loadedCustomers;
    }

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

    private async void OnEditCustomerClicked(object sender, EventArgs e)
    {
        if ((sender as MenuItem)?.CommandParameter is Customer selectedCustomer)
        {
            await Shell.Current.GoToAsync($"EditCustomerPage?CustomerId={selectedCustomer.Id}");
        }
    }
}


