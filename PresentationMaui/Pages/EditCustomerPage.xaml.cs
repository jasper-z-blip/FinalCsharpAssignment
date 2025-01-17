using PresentationMaui.ViewModels;
using Shared.Interfaces;

namespace PresentationMaui.Pages
{
    public partial class EditCustomerPage : ContentPage
    {
        // Viewmodel som hanterar logiken för customers.
        private readonly EditCustomerViewModel _viewModel;

        public EditCustomerPage(ICustomerService customerService)
        {
            InitializeComponent();
            _viewModel = new EditCustomerViewModel(customerService);

            // BindingContext kopplar denna sida till ViewModel så att UI kan uppdateras automatiskt när data ändras.
            BindingContext = _viewModel;
        }

        private async void LoadCustomerButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(_viewModel.CustomerNumber, out int customerNumber))
            {
                // Ska hitta customer med hjälp av dennes kundnummer som användaren skrivit in.
                // Om numret finns så går den in i if-satsen, om numret inte finns så blir de Error och om användaren fyller i tex. 2.5 så blir de Validation Error.
                await _viewModel.LoadCustomerByNumberAsync(customerNumber);
               
                if (_viewModel.CustomerToEdit != null)
                {
                    
                }
                else
                {
                    await DisplayAlert("Error", "Customer not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Validation Error", "Please enter a valid customer number.", "OK");

            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            // Ska spara kund ändringar via ViewModel.
            await _viewModel.SaveCustomerAsync();
            if (_viewModel.CustomerToEdit != null)
            {
                // Om sparning lyckas, visa framgångsmeddelande och rensa formuläret.
                await DisplayAlert("Success", "Customer details updated!", "OK");
                ClearForm();
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            // När användaren klickar på Delete knappen så görs en extra koll att denne verkligen vill radera kontakten, om användaren klickar "Yes" så ¨år den in i if-satsen och kunden raderas.
            var confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this customer?", "Yes", "No");
            if (confirm)
            {
                await _viewModel.DeleteCustomerAsync();
                await DisplayAlert("Success", "Customer deleted successfully!", "OK");

                _viewModel.CustomerToEdit = null;

                ClearForm();
            }
        }

            private void ClearForm()
        {
            _viewModel.CustomerNumber = string.Empty;
            _viewModel.CustomerToEdit = null;
        }
    }
}