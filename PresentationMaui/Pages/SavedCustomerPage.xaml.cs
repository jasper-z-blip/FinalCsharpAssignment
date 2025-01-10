using Shared.Interfaces;
using Shared.Models;
using System.Collections.ObjectModel;

namespace PresentationMaui.Pages
{
    public partial class SavedCustomerPage : ContentPage
    {
        private readonly ICustomerService _customerService;
        private ObservableCollection<Customer> _customers;

        public SavedCustomerPage(ICustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
            _customers = new ObservableCollection<Customer>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            var loadedCustomers = await _customerService.LoadListFromJsonFile();
            _customers = new ObservableCollection<Customer>(loadedCustomers);
            CustomerListView.ItemsSource = _customers;
        }
    }
}

